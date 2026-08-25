using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Features.Auth.Login;
using SafeShare.Application.Features.Auth.Register;
using Wolverine;

namespace SafeShare.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMessageBus bus): ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.UserName, request.Password);
        var token = await bus.InvokeAsync<string>(command, cancellationToken);
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command =
            new RegisterCommand( request.UserName, request.Password, request.PublicKey, request.EncryptedPrivateKey);
        await bus.InvokeAsync(command, cancellationToken);
        return Ok();
    }
}

public record LoginRequest(string UserName, string Password);
public record RegisterRequest(string UserName, string Password, string PublicKey, string EncryptedPrivateKey);