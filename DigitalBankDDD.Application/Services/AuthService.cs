using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Account> _accountRepository;

    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _accountRepository = _unitOfWork.GetRepository<Account>();
    }

    public async Task<AppResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var accountExists = await _accountRepository.GetAsync(a => a.Email == loginRequestDto.EmailOrCpf || a.Cpf == loginRequestDto.EmailOrCpf);
        
        if (accountExists is null)
            return AppResult<LoginResponseDto>.Failure("Account not found.");

        return AppResult<LoginResponseDto>.Success(new LoginResponseDto());
    }
}