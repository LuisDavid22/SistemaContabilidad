using SistemaContabilidad.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Models
{
    public class TypeDbOrCrValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            CuentaContableDto cuenta =(CuentaContableDto) validationContext.ObjectInstance;

           
                if (cuenta.tipo == null)
                {
                   return new ValidationResult("Debe especificar el tipo de movimiento de la cuenta: " + cuenta.cuenta);
                }
                else if (cuenta.tipo.ToLower() != "db" && cuenta.tipo.ToLower() != "cr")
                {
                    return new ValidationResult("Tipo de movimiento incorrecto de la cuenta: " + cuenta.cuenta);
                }

                  
            return ValidationResult.Success;
        }
    }
}