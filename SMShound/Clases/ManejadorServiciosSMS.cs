using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.IO;
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
*/
namespace SMShound.Clases
{
    static class ManejadorServiciosSMS
    {

        public static Entrada[] ObtenerEntradas(string pStrURL)
        {
            Entrada[] lLstResultado = new Entrada[0];

            try
            {
                if (pStrURL.Contains("sms-verification.com"))
                {
                    lLstResultado = ManejadorServiciosSMS.ObtenerEntradasDesdeDominioSmsVerificationCom(pStrURL);
                }
                else if (pStrURL.Contains("receivesmsonline.com"))
                {
                    lLstResultado = ManejadorServiciosSMS.ObtenerEntradasDesdeDominioReceiveSmsOnlineCom(pStrURL);
                }
                else if (pStrURL.Contains("www.receive-sms-online.info"))
                {
                    lLstResultado = ManejadorServiciosSMS.ObtenerEntradasDesdeDominioReceiveSmsOnlineInfo(pStrURL);
                }
                else if (pStrURL.Contains("voskivy.uzdom.com") || pStrURL.Contains("pumpsms.net") || pStrURL.Contains("receivefreesms.com") || pStrURL.Contains("educate.con.sh") || pStrURL.Contains("netlog.smoz.us")
                        || pStrURL.Contains("twin.onamia.biz") || pStrURL.Contains("www.twin.onamia.biz") || pStrURL.Contains("endere.b5m.co"))
                {
                    lLstResultado = ManejadorServiciosSMS.ObtenerEntradasDesdeDominioUzdomCom(pStrURL);
                }
            }
            catch (Exception lObjExcepcion)
            {
                string lStrError = "[" + DateTime.Now + "] Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
            }

            return lLstResultado;
        }

        private static Entrada[] ObtenerEntradasDesdeDominioUzdomCom(string pStrURL)
        {
            List<Entrada> lLstResultado = new List<Entrada>();
            Entrada lObjEntradaAnterior = null;
            Entrada lObjEntrada = null;

            HtmlDocument lObjDocumento = Utilidades.ObtenerDocumentoHTML(pStrURL);
            HtmlNode lObjRegistros = lObjDocumento.GetElementbyId("messages");
            for (int i = 2; i <= lObjRegistros.ChildNodes.Count - 1; i++)
            {
                try
                {
                    if (lObjRegistros.ChildNodes[i].ChildNodes.Count >= 4)
                    {
                        lObjEntrada = new Entrada(pStrURL,
                            DateTime.Now.ToString(),
                            lObjRegistros.ChildNodes[i].ChildNodes[2].InnerText,
                            lObjRegistros.ChildNodes[i].ChildNodes[1].InnerText,
                            lObjRegistros.ChildNodes[i].ChildNodes[3].InnerText,
                            lObjEntradaAnterior);
                        lLstResultado.Add(lObjEntrada);

                        lObjEntradaAnterior = lObjEntrada;
                    }
                }
                catch (Exception lObjExcepcion)
                {
                    string lStrError = "[" + DateTime.Now + "] Error al parsear -" + lObjRegistros.ChildNodes[i].OuterHtml + "-. Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                    Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
                }
            }

            return lLstResultado.ToArray();
        }

        private static Entrada[] ObtenerEntradasDesdeDominioReceiveSmsOnlineInfo(string pStrURL)
        {
            List<Entrada> lLstResultado = new List<Entrada>();
            Entrada lObjEntradaAnterior = null;
            Entrada lObjEntrada = null;

            HtmlDocument lObjDocumento = Utilidades.ObtenerDocumentoHTML(pStrURL);
            HtmlNode lObjRegistros = lObjDocumento.DocumentNode.ChildNodes[4].ChildNodes[3].ChildNodes[5].ChildNodes[7].ChildNodes[1].ChildNodes[3];
            for (int i = 0; i <= lObjRegistros.ChildNodes.Count - 1; i++)
            {
                try
                {
                    if (lObjRegistros.ChildNodes[i].ChildNodes.Count >= 6)
                    {
                        lObjEntrada = new Entrada(pStrURL,
                            DateTime.Now.ToString(),
                            lObjRegistros.ChildNodes[i].ChildNodes[5].InnerText,
                            lObjRegistros.ChildNodes[i].ChildNodes[1].InnerText,
                            lObjRegistros.ChildNodes[i].ChildNodes[3].InnerText,
                            lObjEntradaAnterior);
                        lLstResultado.Add(lObjEntrada);

                        lObjEntradaAnterior = lObjEntrada;
                    }
                }
                catch (Exception lObjExcepcion)
                {
                    string lStrError = "[" + DateTime.Now + "] Error al parsear -" + lObjRegistros.ChildNodes[i].OuterHtml + "-. Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                    Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
                }
            }

            return lLstResultado.ToArray();
        }

        private static Entrada[] ObtenerEntradasDesdeDominioReceiveSmsOnlineCom(string pStrURL)
        {
            List<Entrada> lLstResultado = new List<Entrada>();
            Entrada lObjEntradaAnterior = null;
            Entrada lObjEntrada = null;

            HtmlDocument lObjDocumento = Utilidades.ObtenerDocumentoHTML(pStrURL);
            HtmlNode lObjRegistros = lObjDocumento.GetElementbyId("sms").ChildNodes[1].ChildNodes[3].ChildNodes[3];
            for (int i = 2; i <= lObjRegistros.ChildNodes.Count - 2; i++)
            {
                try
                {
                    lObjEntrada = new Entrada(pStrURL,
                        DateTime.Now.ToString(),
                        lObjRegistros.ChildNodes[i].ChildNodes[2].InnerText,
                        lObjRegistros.ChildNodes[i].ChildNodes[0].InnerText,
                        lObjRegistros.ChildNodes[i].ChildNodes[1].InnerText,
                        lObjEntradaAnterior);
                    lLstResultado.Add(lObjEntrada);

                    lObjEntradaAnterior = lObjEntrada;
                }
                catch (Exception lObjExcepcion)
                {
                    string lStrError = "[" + DateTime.Now + "] Error al parsear -" + lObjRegistros.ChildNodes[i].OuterHtml + "-. Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                    Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
                }
            }

            return lLstResultado.ToArray();
        }

        private static Entrada[] ObtenerEntradasDesdeDominioSmsVerificationCom(string pStrURL)
        {
            List<Entrada> lLstResultado = new List<Entrada>();
            Entrada lObjEntradaAnterior = null;
            Entrada lObjEntrada = null;

            HtmlDocument lObjDocumento = Utilidades.ObtenerDocumentoHTML(pStrURL);
            HtmlNode lObjRegistros = lObjDocumento.GetElementbyId("messages").ChildNodes[1];
            for (int i = 2; i <= lObjRegistros.ChildNodes.Count - 2; i++)
            {
                try
                {
                    lObjEntrada = new Entrada(pStrURL,
                        DateTime.Now.ToString(),
                        lObjRegistros.ChildNodes[i].ChildNodes[2].InnerText,
                        lObjRegistros.ChildNodes[i].ChildNodes[1].InnerText,
                        lObjRegistros.ChildNodes[i].ChildNodes[3].InnerText,
                        lObjEntradaAnterior);
                    lLstResultado.Add(lObjEntrada);

                    lObjEntradaAnterior = lObjEntrada;
                }
                catch (Exception lObjExcepcion)
                {
                    string lStrError = "[" + DateTime.Now + "] Error al parsear -" + lObjRegistros.ChildNodes[i].OuterHtml + "-. Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                    Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
                }
            }

            return lLstResultado.ToArray();
        }
    }
}
