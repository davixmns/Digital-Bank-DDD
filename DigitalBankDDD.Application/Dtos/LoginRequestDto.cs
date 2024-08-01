using System.ComponentModel.DataAnnotations;
using DigitalBankDDD.Application.Validations;

namespace DigitalBankDDD.Application.Dtos;

public class LoginRequestDto
{
    [LoginRequestInputValidation]
    public string EmailOrCpf { get; set; } = string.Empty;
    
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}