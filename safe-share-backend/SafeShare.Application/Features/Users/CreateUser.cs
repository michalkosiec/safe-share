using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Users;

public record CreateUserCommand(Guid Id, string Name, string Password, string PublicKey, string EncryptedPrivateKey);

public class CreateUserHandler
{
    public async Task HandleAsync(CreateUserCommand command, IUserRepository repo)
    {
        // Must be fixed!
        var passwordHash = command.Password;
        
        var user = new User(command.Name, passwordHash, command.PublicKey, command.EncryptedPrivateKey);
        
        // Must account for race condition
        await repo.CreateAsync(user);
        await repo.SaveChangesAsync();
    }
}