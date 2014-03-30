using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;


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
    class Entrada
    {
        private const string SEPARADOR = "\t";

        public string Servicio { get; private set; }
        public string InstanteCaptura { get; private set; }
        public string InstanteMensaje { get; private set; }
        public string Origen { get; private set; }
        public string Contenido { get; private set; }
        public string Identificador { get; private set; }

        public static string ObtenerEstructuraEntrada()
        {
            return "SERVICIO" + SEPARADOR
                + "IDENTIFICADOR" + SEPARADOR
                + "INSTANTE CAPTURA" + SEPARADOR
                + "INSTANTE MENSAJE" + SEPARADOR
                + "ORIGEN" + SEPARADOR
                + "CONTENIDO" + SEPARADOR
                + Environment.NewLine;
        }

        private Entrada() { }

        public Entrada(string pStrRegistro)
        {
            string[] lArrTokens = pStrRegistro.Split(new char[] { '\t' });
            this.Servicio = lArrTokens[0];
            this.Identificador = lArrTokens[1];
            this.InstanteCaptura = lArrTokens[2];
            this.InstanteMensaje = lArrTokens[3];
            this.Origen = lArrTokens[4];
            this.Contenido = lArrTokens[5];
        }

        public Entrada(string pStrServicio, string pStrInstanteCaptura, string pStrInstanteMensaje, string pStrOrigen, string pStrContenido, Entrada lObjEntradaAnterior)
        {
            this.Servicio = HttpUtility.UrlDecode(pStrServicio);
            this.InstanteCaptura = pStrInstanteCaptura;
            this.InstanteMensaje = Utilidades.LimpiarContenidoSMS(pStrInstanteMensaje);
            this.Origen = Utilidades.LimpiarContenidoSMS(pStrOrigen);
            this.Contenido = Utilidades.LimpiarContenidoSMS(pStrContenido);
            this.Identificador = this.ObtenerIdentificador((lObjEntradaAnterior == null) ? "" : lObjEntradaAnterior.Contenido);
        }

        private string ObtenerIdentificador(string pStrContenidoAnterior = "")
        {
            return Utilidades.ConvertirArrayBytesAString(
                new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(
                    this.Servicio + this.Origen + this.Contenido + HttpUtility.HtmlDecode(pStrContenidoAnterior)
                )));
        }

        public override string ToString()
        {
            return this.Servicio + SEPARADOR
                + this.Identificador + SEPARADOR
                + this.InstanteCaptura + SEPARADOR
                + this.InstanteMensaje + SEPARADOR
                + this.Origen + SEPARADOR
                + this.Contenido + Environment.NewLine;
        }
    }
}
