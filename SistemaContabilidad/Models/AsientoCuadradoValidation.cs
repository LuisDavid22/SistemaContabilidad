using SistemaContabilidad.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Models
{
    public class AsientoCuadradoValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AsientoContableDto asiento = (AsientoContableDto)validationContext.ObjectInstance;

            double TotalDebitos = 0;
            double TotalCreditos = 0;
            if (asiento.Cuentas != null)
            {
                foreach (var cuenta in asiento.Cuentas)
                {
                    if (cuenta.tipo.ToLower() == "db")
                    {
                        TotalDebitos += cuenta.monto;
                    }
                    else if (cuenta.tipo.ToLower() == "cr")
                    {
                        TotalCreditos += cuenta.monto;
                    }
                }
                if (TotalCreditos != TotalDebitos)
                {
                    return new ValidationResult("");
                }
         
            }
            else
            {
                return new ValidationResult("");
            }


            return ValidationResult.Success;
        }
    }
}