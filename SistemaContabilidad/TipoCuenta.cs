//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaContabilidad
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoCuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoCuenta()
        {
            this.CuentaContable = new HashSet<CuentaContable>();
        }
    
        public int idTipoCuenta { get; set; }
        public string Descripcion { get; set; }
        public string Origen { get; set; }
        public string Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
    }
}
