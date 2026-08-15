using SafeShare.Application.Common.Interfaces;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Users;

public record CreateUserCommand(string Name, string Password, string PublicKey, string EncryptedPrivateKey);

public class CreateUserHandler(IPasswordHasher passwordHasher, IUserRepository repo)
{
    public async Task<UserResponse> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var passwordHash = passwordHasher.Hash(command.Password);
        
        var user = new User(command.Name, passwordHash, command.PublicKey, command.EncryptedPrivateKey);
        
        // Must account for race condition
        await repo.CreateAsync(user, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);
        
        var createdUser = await repo.GetAsync(user.Id, cancellationToken);
        if (createdUser == null)
            throw new InvalidOperationException("User not created");
        
        var userResponse = new UserResponse(createdUser.Id, createdUser.Name, createdUser.PublicKey, createdUser.EncryptedPrivateKey);

        return userResponse;
    }
}