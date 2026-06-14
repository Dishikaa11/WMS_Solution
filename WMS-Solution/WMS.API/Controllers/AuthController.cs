using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using System.Security.Claims;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    //[AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized("Invalid Username or Password");

        return Ok(result);
    }

    //[Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
    ChangePasswordDto dto)
    {
        var username = dto.Username;

        if (username == null)
            return Unauthorized();

        var result =
            await _authService
                .ChangePasswordAsync(username, dto);

        if (!result)
            return BadRequest(
                "Current password is incorrect");

        return Ok(new
        {
            success = true,
            message = "Password changed successfully"
        });
    }
}