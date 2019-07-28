using SistemaContabilidad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Dto
{
    public class AsientoContableDto
    {
        public int idAsiento { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [IdAuxiliarValidoValidation]
        public int Auxiliar { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Estado { get; set; }
        [Required]
        [IdCuentaIgualesValidation]
        [AsientoCuadradoValidation]
        public List<CuentaContableDto> Cuentas { get; set; }
    }
}