using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConvalidacionEducacionSuperior.Models
{
    public class PagoRequest
    {
        public string codAplicacion { get; set; }
        public string codServicio { get; set; }
        public string referenciaPago { get; set; }
        public string tipoDocumento { get; set; }
        public string noDocumento { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string email { get; set; }
        public string urlRedirectOri { get; set; }
    }

    public class PagoResponse
    {
        public string URLRedirect { get; set; }
        public string AdditionalInfoArray { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnDesc { get; set; }
    }
}