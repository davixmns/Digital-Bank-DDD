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


    public async Task<ApiResult<LoginResponseDto>> LoginWithEmailAsync(LoginRequestDto loginRequestDto)
    {
        var accountExists = await _accountRepository.GetAsync(a => a.Email == loginRequestDto.EmailOrCpf);
        
        if (accountExists is null)
            return ApiResult<LoginResponseDto>.Failure("Account not found.");
        
        return ApiResult<LoginResponseDto>.Success(new LoginResponseDto());
    }

    public async Task<ApiResult<LoginResponseDto>> LoginWithCpfAsync(LoginRequestDto loginRequestDto)
    {
        var accountExists = await _accountRepository.GetAsync(a => a.Cpf == loginRequestDto.EmailOrCpf);
        
        if (accountExists is null)
            return ApiResult<LoginResponseDto>.Failure("Account not found.");
        
        return ApiResult<LoginResponseDto>.Success(new LoginResponseDto());
    }
}