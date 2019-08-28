using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvalidacionEducacionSuperior.Models
{
    public class ParametrosEntrada
    {
        public string codRefPago { get; set; }
        public string codServicio { get; set; }
        public int codBanco { get; set; }
        public int codPlataforma { get; set; }
        public valorMonetario valorMonetario { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string codEntidadFinanciera { get; set; }
        public string comentarios { get; set; }
        public string codMedioPago { get; set; }

    }

    public class valorMonetario
    {
        public double valorTransaccion { get; set; }
        public string moneda { get; set; }
    }
}
