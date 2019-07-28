using SistemaContabilidad.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Models
{
    public class IdCuentaValidoValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CuentaContableDto cuenta = (CuentaContableDto)validationContext.ObjectInstance;
            ContabilidadEntities1 db = new ContabilidadEntities1();

        

            List<int> IdValidos = db.CuentaContable.Select(c => c.idCuentaContable).ToList();

            if (!IdValidos.Contains(cuenta.id))
            {
                return new ValidationResult("El id de esta cuenta es invalido");
            }

            return ValidationResult.Success;
            
        }
    }
}