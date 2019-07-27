using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Dto
{
    public class CuentaContableDto
    {
        public int id { get; set; }
        public string cuenta { get; set; }
        public string tipo { get; set; }
        public double monto { get; set; }
    }
}