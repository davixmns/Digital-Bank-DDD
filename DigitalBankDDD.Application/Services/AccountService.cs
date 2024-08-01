using AutoMapper;
using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountDomainService _accountDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Account> _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(IAccountDomainService accountDomainService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _accountDomainService = accountDomainService;
        _unitOfWork = unitOfWork;
        _accountRepository = unitOfWork.GetRepository<Account>();
        _mapper = mapper;
    }

    public async Task<AppResult<AccountResponseDto>> CreateAccountAsync(Account account)
    {
        _accountDomainService.ValidateAccountCreation(account);

        var accountExists = await _accountRepository.GetAsync(a => a.Cpf == account.Cpf || a.Email == account.Email);

        if (accountExists is not null)
            return AppResult<AccountResponseDto>.Failure("Account already exists.");

        var createdAccount = _accountRepository.Save(account);

        await _unitOfWork.CommitAsync();
        
        var accountDto = _mapper.Map<AccountResponseDto>(createdAccount);

        return AppResult<AccountResponseDto>.Success(accountDto);
    }
}