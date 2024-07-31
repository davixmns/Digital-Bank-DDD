using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;

namespace DigitalBankDDD.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResult<LoginResponseDto>> LoginWithEmailAsync(LoginRequestDto loginRequestDto); 
    Task<ApiResult<LoginResponseDto>> LoginWithCpfAsync(LoginRequestDto loginRequestDto);
}