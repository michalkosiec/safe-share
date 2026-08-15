using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Features.Users;
using Wolverine;

namespace SafeShare.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController(IMessageBus bus) : ControllerBase
{
    // [HttpGet("{id:Guid}")]
    // public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    // {
    // }
    //
    // [HttpGet]
    // public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    // {
    // }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserCreateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.Name, request.Password, request.PublicKey,
            request.EncryptedPrivateKey);
        
        await bus.InvokeAsync(command, cancellationToken);
        return Ok();
    }
    
    public record UserCreateRequest(string Name, string Password, string PublicKey, string EncryptedPrivateKey);
    public record UserUpdateRequest(string Name, string Password, string PublicKey, string EncryptedPrivateKey);
}