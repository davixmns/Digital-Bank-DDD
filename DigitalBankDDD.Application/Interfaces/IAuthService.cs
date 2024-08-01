using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;

namespace DigitalBankDDD.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto); 
}