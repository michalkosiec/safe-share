using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Application.Features.Auth.DTOs;
using SafeShare.Application.Features.Auth.Login;
using SafeShare.Application.Features.Auth.Register;
using Wolverine;

namespace SafeShare.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMessageBus bus, ICurrentUserService currentUserService): ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = currentUserService.UserId;
        var username = currentUserService.Username;
        
        var currentUserResponse = new CurrentUserResponse(userId, username);
        return Ok(currentUserResponse);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Username, request.Password);
        var response = await bus.InvokeAsync<LoginResponse>(command, cancellationToken);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(2)
        };
        
        Response.Cookies.Append("jwt_token", response.token, cookieOptions);
        
        var currentUserResponse = new CurrentUserResponse(response.UserId, response.Username);
        return Ok(currentUserResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command =
            new RegisterCommand( request.Username, request.Password, request.PublicKey, request.EncryptedPrivateKey);
        await bus.InvokeAsync(command, cancellationToken);
        return Ok();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
        
        Response.Cookies.Delete("jwt_token", cookieOptions);
        return Ok();
    }
}

public record LoginRequest(string Username, string Password);
public record RegisterRequest(string Username, string Password, string PublicKey, string EncryptedPrivateKey);
public record CurrentUserResponse(Guid UserId, string Username);