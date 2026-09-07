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
        if (string.IsNullOrEmpty(username) || userId == Guid.Empty || userId == null)
            return Unauthorized("No valid claims in the token.");
        
        var currentUserResponse = new CurrentUserResponse(userId.Value, username);
        return Ok(currentUserResponse);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Username, request.Password);
        var response = await bus.InvokeAsync<LoginResponse>(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command =
            new RegisterCommand( request.Username, request.Password, request.PublicKey, request.EncryptedPrivateKey);
        await bus.InvokeAsync(command, cancellationToken);
        return Ok();
    }
}

public record LoginRequest(string Username, string Password);
public record RegisterRequest(string Username, string Password, string PublicKey, string EncryptedPrivateKey);
public record CurrentUserResponse(Guid UserId, string Username);