using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Application.Interfaces;

public interface IAccountService
{
    Task<AppResult<AccountResponseDto>> CreateAccountAsync(Account account);
}