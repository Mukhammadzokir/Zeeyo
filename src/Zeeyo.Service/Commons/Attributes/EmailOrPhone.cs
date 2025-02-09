using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Service.Commons.Attributes;

public class EmailOrPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Please enter an email or phone number.");
        }

        var input = value.ToString();

        // Regular expressions for email and phone number validation
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        var phoneRegex = new Regex(@"^\+?\d{9,15}$"); // Supports optional "+" and 9-15 digits

        if (emailRegex.IsMatch(input) || phoneRegex.IsMatch(input))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Please enter a valid email or phone number.");
    }
}

