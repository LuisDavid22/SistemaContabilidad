using SistemaContabilidad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Dto
{
    public class CuentaContableDto
    {
        
        [IdCuentaValidoValidation]
        public int id { get; set; }
        public string cuenta { get; set; }
        [TypeDbOrCrValidation]
        public string tipo { get; set; }
        public double monto { get; set; }

      
    }
}