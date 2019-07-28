using SistemaContabilidad.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Models
{
    public class IdCuentaIgualesValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AsientoContableDto asiento =  (AsientoContableDto)validationContext.ObjectInstance;

            foreach (var cuenta in asiento.Cuentas)
            {
                if(asiento.Cuentas.Where(c => c.id == cuenta.id).ToList().Count > 1)
                {
                    return new ValidationResult("");
                }
            }

            return ValidationResult.Success;
        }
    }
}