using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;

namespace DigitalBankDDD.Application.Interfaces;

public interface IAuthService
{
    Task<AppResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto); 
}