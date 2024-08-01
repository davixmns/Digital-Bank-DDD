using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DigitalBankDDD.Application.Validations;

public class LoginRequestInputValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var emailOrCpf = (string) value;
        
        if(string.IsNullOrEmpty(emailOrCpf))
            return new ValidationResult("Email or CPF is required");
        
        var emailAdressAttribute = new EmailAddressAttribute();
        var cpfRegex = new Regex(@"^\d{11}$");
        
        if (!emailAdressAttribute.IsValid(emailOrCpf) && !cpfRegex.IsMatch(emailOrCpf))
            return new ValidationResult("Invalid Email or CPF");

        return ValidationResult.Success!;
    }
    
}