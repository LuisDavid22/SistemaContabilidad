﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ContabilidadEntities : DbContext
    {
        public ContabilidadEntities()
            : base("name=ContabilidadEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Auxiliar> Auxiliar { get; set; }
        public virtual DbSet<CuentaContable> CuentaContable { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }
    }
}
