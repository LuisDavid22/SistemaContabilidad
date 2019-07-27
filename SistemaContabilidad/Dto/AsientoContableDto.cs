using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Dto
{
    public class AsientoContableDto
    {
        public int idAsiento { get; set; }
        public string Descripcion { get; set; }
        public int Auxiliar { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public List<CuentaContableDto> Cuentas { get; set; }
    }
}