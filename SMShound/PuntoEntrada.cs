using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using SMShound.Clases;
using System.Threading;

/**
    Copyright (C) 2014 Miguel Angel García
  
    This file is part of SMShound.

    SMShound is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SMShound is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with SMShound.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Icon by Valera Zvonko ( http://creativecommons.org/licenses/by/3.0/ )
*/
namespace SMShound
{
    class PuntoEntrada
    {
        private const string FICHERO_PALABRAS_CLAVE = "";

        private static SortedSet<string> Mensajes { get; set; }
        private static string[] PalabrasClave { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                Mensajes = new SortedSet<string>();
                CargarMensajesPrevios();
                CargarPalabrasClave();
                Procesar();
            }
            catch (Exception lObjExcepcion)
            {
                Console.WriteLine("Se capturó la siguiente excepción: {0}", lObjExcepcion.ToString());
                string lStrError = "[" + DateTime.Now + "] Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
            }

            Console.WriteLine(Environment.NewLine + Environment.NewLine + "Presione ENTER para finalizar.");
            Console.ReadLine();
        }

        private static void CargarPalabrasClave()
        {
            if (File.Exists(SMShound.Properties.Settings.Default.FicheroPalabrasClave))
            {
                PalabrasClave = File.ReadAllLines(SMShound.Properties.Settings.Default.FicheroPalabrasClave);
            }
            else
            {
                PalabrasClave = new string[0];
            }
        }

        private static void CargarMensajesPrevios()
        {
            if (File.Exists(SMShound.Properties.Settings.Default.FicheroSalida))
            {
                foreach (string lStrLinea in File.ReadAllLines(SMShound.Properties.Settings.Default.FicheroSalida).Skip(1))
                {
                    try
                    {
                        Mensajes.Add(new Entrada(lStrLinea).Identificador);
                    }
                    catch (Exception lObjExcepcion)
                    {
                        string lStrError = "[" + DateTime.Now + "] Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                        Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
                    }
                }
            }
        }

        private static void Procesar()
        {
            string[] lArrEnlaces = File.ReadAllLines(SMShound.Properties.Settings.Default.FicheroEnlaces);
            string lStrRutaFicheroSalida = SMShound.Properties.Settings.Default.FicheroSalida;

            if (!File.Exists(lStrRutaFicheroSalida))
            {
                Utilidades.EscribirFichero(lStrRutaFicheroSalida, Entrada.ObtenerEstructuraEntrada());
            }

            SortedSet<string> lSetEntradas = new SortedSet<string>();
            while (true)
            {
                Console.WriteLine("=> Resultado de captura de SMS:");

                foreach (string lStrEnlace in lArrEnlaces)
                {
                    Thread lObjHilo = new Thread(new ParameterizedThreadStart(PuntoEntrada.ProcesarEnHilo));
                    lObjHilo.Start(lStrEnlace);
                }

                Thread.Sleep(SMShound.Properties.Settings.Default.SegundosEntreCiclos * 1000);
                Console.WriteLine();
            }
        }

        private static void ProcesarEnHilo(object pObjURL)
        {
            if (pObjURL is string)
            {
                string lStrEnlace = (string)pObjURL;
                string lStrRutaFicheroSalida = SMShound.Properties.Settings.Default.FicheroSalida;

                Entrada[] lArrEntradas = ManejadorServiciosSMS.ObtenerEntradas(lStrEnlace);
                StringBuilder lObjCadena = new StringBuilder();
                StringBuilder lObjCadenaCorreo = new StringBuilder();
                Entrada lObjEntradaAnterior = null;
                int lIntMensajesRepetidosConsecutivos = 0;
                int lIntEntradasIncorporadas = 0;
                bool lBlnEncontradasPalabrasClave = false;

                foreach (Entrada lObjEntrada in lArrEntradas)
                {
                    lock (Mensajes)
                    {
                        if (!Mensajes.Contains(lObjEntrada.Identificador))
                        {
                            foreach (string lStrPalabraClave in PuntoEntrada.PalabrasClave)
                            {
                                if (lObjEntrada.Contenido.Contains(lStrPalabraClave))
                                {
                                    lBlnEncontradasPalabrasClave = true;
                                    lObjCadenaCorreo.Append("(" + lStrPalabraClave + ") " + lObjEntrada.ToString());
                                }
                            }

                            lIntMensajesRepetidosConsecutivos = 0;
                            lIntEntradasIncorporadas++;
                            Mensajes.Add(lObjEntrada.Identificador);
                            lObjCadena.Insert(0, lObjEntrada);
                        }
                        else
                        {
                            lIntMensajesRepetidosConsecutivos++;
                            if (lIntMensajesRepetidosConsecutivos > SMShound.Properties.Settings.Default.MaximoMensajesRepetidosHastaFin)
                            {
                                break;
                            }
                        }
                    }

                    lObjEntradaAnterior = lObjEntrada;
                }

                if (lBlnEncontradasPalabrasClave)
                {
                    Utilidades.EnviarCorreoElectronico(
                        SMShound.Properties.Settings.Default.SMTPservidor,
                        SMShound.Properties.Settings.Default.SMTPpuerto,
                        SMShound.Properties.Settings.Default.SMTPusuario,
                        SMShound.Properties.Settings.Default.SMTPclave,
                        SMShound.Properties.Settings.Default.SMTPusarSSL,
                        false,
                        SMShound.Properties.Settings.Default.SMTPusuario,
                        new string[] { SMShound.Properties.Settings.Default.SMTPusuario },
                        "SMShound ha encontrado palabras clave",
                        lObjCadenaCorreo.ToString());
                }

                Utilidades.EscribirFichero(lStrRutaFicheroSalida, lObjCadena.ToString());

                Console.WriteLine(" ({0}{1} SMS) {2}",
                    lBlnEncontradasPalabrasClave ? "@" : "",
                     lIntEntradasIncorporadas.ToString(),
                     lStrEnlace);
            }
        }
    }
}
