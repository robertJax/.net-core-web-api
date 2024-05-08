﻿using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Validation
{
	public class DateCheckAttribute : ValidationAttribute
	{
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if(date < DateTime.Now)
            {
                return new ValidationResult("The date must be greater than or equal to todays date");
            }

            return ValidationResult.Success;
        }
    }
}
