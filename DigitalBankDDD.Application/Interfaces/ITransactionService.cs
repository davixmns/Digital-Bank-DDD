using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Application.Interfaces;

public interface ITransactionService
{
    Task<AppResult<TransactionResponseDto>> CreateTransactionAsync(TransactionRequestDto transactionRequestDto);
}