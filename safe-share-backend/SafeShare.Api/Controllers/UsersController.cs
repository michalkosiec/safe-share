using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Features.Users;
using SafeShare.Application.Features.Users.CreateUser;
using SafeShare.Application.Features.Users.DeleteUser;
using SafeShare.Application.Features.Users.DTOs;
using SafeShare.Application.Features.Users.GetAllUsers;
using SafeShare.Application.Features.Users.GetUser;
using SafeShare.Application.Features.Users.UpdateUser;
using Wolverine;

namespace SafeShare.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController(IMessageBus bus) : ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetUserQuery(id);
        var userResponse = await bus.InvokeAsync<UserResponse>(command, cancellationToken);
        return Ok(userResponse);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var command = new GetAllUsersQuery();
        var usersResponse = await bus.InvokeAsync<IEnumerable<UserResponse>>(command, cancellationToken);
        return Ok(usersResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserCreateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.Name, request.Password, request.PublicKey,
                                           request.EncryptedPrivateKey);
        
        var userResponse = await bus.InvokeAsync<UserResponse>(command, cancellationToken);
        return Created($"/api/users/{request.Name}", userResponse);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] UserUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(id, request.Name, request.PublicKey, request.EncryptedPrivateKey);
        var userResponse = await bus.InvokeAsync<UserResponse>(command, cancellationToken);
        
        return  Ok(userResponse);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        await bus.InvokeAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    public record UserCreateRequest(string Name, string Password, string PublicKey, string EncryptedPrivateKey);
    public record UserUpdateRequest(string Name, string PublicKey, string EncryptedPrivateKey);
}