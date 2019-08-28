using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConvalidacionEducacionSuperior.Models
{
    public class SolicitudModel
    {
        public long NroSolicitud { get; set; }
        public string TipoSolicitud { get; set; }
        public string Estado { get; set; }
        public string DocumentoUsuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime FechaSolicitud { get; set; }
        public double valor { get; set; }
    }
}