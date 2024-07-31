using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Services;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankDDD.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public readonly IAuthService _authService;
    
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        ApiResult<LoginResponseDto> response;
        
        if (loginRequestDto.IsEmail())
            response = await _authService.LoginWithEmailAsync(loginRequestDto);
        else if (loginRequestDto.IsCpf())
            response = await _authService.LoginWithCpfAsync(loginRequestDto);
        else
            return BadRequest("Invalid Email or CPF");
        
        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }


}