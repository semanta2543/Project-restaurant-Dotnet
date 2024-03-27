using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using WebApplication3.Data;

namespace restSakei.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueContactNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string contactNumber = value.ToString();

                if (!contactNumber.StartsWith("06") || contactNumber.Length != 10)
                {
                    return new ValidationResult("Numri i kontaktit duhet te filloje me '06' dhe të ketë saktësisht 10 karaktere.");
                }

                var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

                // Kontrollo nëse numri i kontaktit është unik
                if (dbContext.Staf.Any(s => s.ContactNumber == contactNumber && s.Id != (int)validationContext.ObjectInstance.GetType().GetProperty("Id").GetValue(validationContext.ObjectInstance)))
                {
                    return new ValidationResult("Numri i kontaktit duhet të jetë unik.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
