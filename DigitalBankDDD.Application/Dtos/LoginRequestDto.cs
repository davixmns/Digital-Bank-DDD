using System.ComponentModel.DataAnnotations;

namespace DigitalBankDDD.Application.Dtos;

public class LoginRequestDto
{
    [Required] //email Or CPF
    public string EmailOrCpf { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    public bool IsEmail()
    {
        var emailAdressAttribute = new EmailAddressAttribute();
        return emailAdressAttribute.IsValid(EmailOrCpf);
    }
    
    public bool IsCpf()
    {
        return !IsEmail() && EmailOrCpf.Length is 11;
    }
}