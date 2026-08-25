using SafeShare.Application.Common.Interfaces;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Auth.Register;

public class RegisterCommandHandler(IUserRepository repo, IPasswordHasher passwordHasher)
{
    public async Task HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await repo.GetByUserNameAsync(command.UserName, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"User {command.UserName} already exists");
        
        var hashedPassword = passwordHasher.Hash(command.Password);
        
        var newUser = new User(command.UserName, hashedPassword,  command.PublicKey, command.EncryptedPrivateKey);
        
        await repo.CreateAsync(newUser, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);
    }
}