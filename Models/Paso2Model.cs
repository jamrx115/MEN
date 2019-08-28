using ConvalidacionEducacionSuperiorDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConvalidacionEducacionSuperior.Models
{
    public class Paso2Model
    {
        public bool notificaElectrinica { get; set; }
        public bool notificaTercero { get; set; }
        public string displayNotifica { get; set; }
        public bool institucionesExtrangeras { get; set; }
        public bool beca { get; set; }
        public bool convenio { get; set; }
        public bool preGradoValidado { get; set; }
        public bool esPregrado { get; set; }
        public string convalidacion { get; set; }
        public string estilo { get; set; }
        public string estilo_1 { get; set; }
        public double valor { get; set; }
        public string valorString { get; set; }
        public bool nacional { get; set; }
        public tbSolicitud solicitud { get; set; }
    }

    
}