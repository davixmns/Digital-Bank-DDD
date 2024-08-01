using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankDDD.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var response = await _authService.LoginAsync(loginRequestDto);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

}