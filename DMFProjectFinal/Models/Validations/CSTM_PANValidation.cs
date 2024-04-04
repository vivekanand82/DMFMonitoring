using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DMFProjectFinal.Models.Validations
{
    public class CSTM_PANValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var displayName = GetDisplayName(validationContext);
                if (!Regex.IsMatch(value.ToString(), @"^[A-Z]{5}[0-9]{4}[A-Z]{1}$"))
                {
                    return new ValidationResult($"Invalid {displayName}.");
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