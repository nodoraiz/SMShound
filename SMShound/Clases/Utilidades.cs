using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using HtmlAgilityPack;
using System.Net;
using System.Web;
using System.Net.Mail;


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
    static class Utilidades
    {
        private static Mutex cObjMutex = null;
        private static Mutex Mutex
        {
            get
            {
                if (Utilidades.cObjMutex == null)
                {
                    Utilidades.cObjMutex = new Mutex();
                }

                return Utilidades.cObjMutex;
            }
        }

        public static string ConvertirArrayBytesAString(byte[] pArrBytes)
        {
            StringBuilder lObjStringBuilder = new StringBuilder(pArrBytes.Length);
            for (int i = 0; i < pArrBytes.Length - 1; i++)
            {
                lObjStringBuilder.Append(pArrBytes[i].ToString("X2"));
            }
            return lObjStringBuilder.ToString();
        }

        public static void EscribirFichero(string pStrRuta, string pStrDato, bool pBlnSobreescribir = false)
        {
            Utilidades.Mutex.WaitOne();
            if (pBlnSobreescribir)
            {
                File.WriteAllText(pStrRuta, pStrDato);
            }
            else
            {
                File.AppendAllText(pStrRuta, pStrDato);
            }
            Utilidades.Mutex.ReleaseMutex();
        }

        public static HtmlDocument ObtenerDocumentoHTML(string pStrURL)
        {
            HttpWebRequest lObjPeticion = (HttpWebRequest)HttpWebRequest.Create(pStrURL);
            HttpWebResponse lObjRespuesta = (HttpWebResponse)lObjPeticion.GetResponse();
            HtmlDocument lObjDocumento = new HtmlDocument();
            lObjDocumento.Load(lObjRespuesta.GetResponseStream());
            lObjRespuesta.Close();

            return lObjDocumento;
        }

        public static string LimpiarContenidoSMS(string pStrContenido)
        {
            return HttpUtility.HtmlDecode(pStrContenido).Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ");
        }

        public static void EnviarCorreoElectronico(string pStrServidorSMTP, int pIntPuertoServicioSMTP, string pStrUsuarioSMTP, string pStrClaveSMTP, bool pBlnEnableSsl, bool pBlnIsBodyHtml, string pStrDireccionRemitente,
            string[] pLstDestinatarios, string pStrAsuntoCorreo, string pStrTextoCorreo)
        {
            if (!string.IsNullOrEmpty(pStrServidorSMTP) && !string.IsNullOrEmpty(pStrUsuarioSMTP) && !string.IsNullOrEmpty(pStrClaveSMTP))
            {
                SmtpClient lObjClienteSMTP = new SmtpClient();
                MailMessage lObjCorreoElectronico = new MailMessage();
                NetworkCredential lObjCredencialesSMTP = null;

                lObjClienteSMTP.Host = pStrServidorSMTP;
                lObjClienteSMTP.Port = pIntPuertoServicioSMTP;
                lObjClienteSMTP.UseDefaultCredentials = false;
                lObjClienteSMTP.EnableSsl = pBlnEnableSsl;

                if (pStrUsuarioSMTP != "") lObjCredencialesSMTP = new NetworkCredential(pStrUsuarioSMTP, pStrClaveSMTP);

                if (lObjCredencialesSMTP != null) lObjClienteSMTP.Credentials = lObjCredencialesSMTP;

                lObjCorreoElectronico.From = new MailAddress(pStrDireccionRemitente);
                lObjCorreoElectronico.Subject = pStrAsuntoCorreo;
                lObjCorreoElectronico.IsBodyHtml = pBlnIsBodyHtml;
                lObjCorreoElectronico.Body = pStrTextoCorreo;

                foreach (string lstrDestinatario in pLstDestinatarios)
                {
                    lObjCorreoElectronico.To.Add(lstrDestinatario);
                }

                try
                {
                    lObjClienteSMTP.Send(lObjCorreoElectronico);
                }
                catch (Exception lObjExcepcion)
                {
                    string lStrError = "[" + DateTime.Now + "] Excepción:" + Environment.NewLine + lObjExcepcion.ToString() + Environment.NewLine + Environment.NewLine;
                    Utilidades.EscribirFichero(SMShound.Properties.Settings.Default.FicheroError, lStrError);
                }
            }
        }
    }
}
