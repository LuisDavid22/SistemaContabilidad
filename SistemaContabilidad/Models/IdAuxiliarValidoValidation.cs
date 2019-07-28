using SistemaContabilidad.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidad.Models
{
    public class IdAuxiliarValidoValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AsientoContableDto asiento = (AsientoContableDto)validationContext.ObjectInstance;
            ContabilidadEntities1 db = new ContabilidadEntities1();

        

            List<int> IdValidos = db.Auxiliar.Select(c => c.idAuxiliar).ToList();

            if (!IdValidos.Contains(asiento.Auxiliar))
            {
                return new ValidationResult("El id del auxiliar es invalido");
            }

            return ValidationResult.Success;
            
        }
    }
}