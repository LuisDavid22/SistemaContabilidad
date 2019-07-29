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
        [Range(1,double.MaxValue,ErrorMessage ="El monto de las cuentas debe ser mayor que cero")]
        public double monto { get; set; }

      
    }
}