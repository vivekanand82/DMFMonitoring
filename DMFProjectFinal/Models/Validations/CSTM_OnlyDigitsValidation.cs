﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;


namespace DMFProjectFinal.Models.Validations
{
    public class CSTM_OnlyDigitsValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var displayName = GetDisplayName(validationContext);
                if (!Regex.IsMatch(value.ToString(), @"^[0-9]*$"))
                {
                    return new ValidationResult($"Only numbers are allowed in {displayName} field.");
                }
            }
            return ValidationResult.Success;
        }

        private string GetDisplayName(ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            if (property != null)
            {
                var displayNameAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (displayNameAttribute != null)
                {
                    return displayNameAttribute.Name;
                }
            }
            return validationContext.DisplayName;
        }
    }
}