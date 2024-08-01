using System.ComponentModel.DataAnnotations;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Wrapper;

namespace DigitalBankDDD.Domain.Entities;

public class Account : BaseEntity
{
    public string AccountNumber { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10);
    
    //se colocar como privado o EF core não consegue mapear
    public decimal Balance { get; private set; } = 0;
    
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string? Email { get; set; }
    
    [StringLength(20)]
    [RegularExpression(@"\d{11}", ErrorMessage = "Invalid CPF size")]
    public string Cpf { get; init; } = string.Empty;
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    
    [StringLength(20)]
    [RegularExpression(@"^(\+[0-9]{2,3}\s?)?\(?[0-9]{2}\)?\s?[0-9]{5}-?[0-9]{4}$", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    
    [Required] 
    [MinLength(8)] 
    public string? Password { get; set; } = string.Empty;

    public DomainResult TransferTo(Account destinationAccount, decimal amount)
    {
        if(Equals(destinationAccount))
            return DomainResult.Failure("The source and destination accounts must be different.");
        
        var withdrawResult = Withdraw(amount);
        
        if (!withdrawResult.IsSuccess)
            return withdrawResult;
        
        var depositResult = destinationAccount.Deposit(amount);
        
        if (!depositResult.IsSuccess)
        {
            Deposit(amount);
            return depositResult;
        }
        
        return DomainResult.Success();
    }
    
    private DomainResult Deposit(decimal amount)
    {
        if (amount <= 0)
            return DomainResult.Failure("The amount must be greater than zero.");
        
        Balance += amount;
        
        return DomainResult.Success();
    }

    private DomainResult Withdraw(decimal amount)
    {
        if (amount <= 0)
            return DomainResult.Failure("The amount must be greater than zero.");

        if (!HasBalance(amount).IsSuccess)
            return DomainResult.Failure("Insufficient balance.");
        
        Balance -= amount;
        
        return DomainResult.Success();
    }
    
    private DomainResult HasBalance(decimal amount)
    {
        if (amount <= 0)
            return DomainResult.Failure("The amount must be greater than zero.");

        return Balance >= amount ?
            DomainResult.Success() :
            DomainResult.Failure("Insufficient balance.");
    }
}