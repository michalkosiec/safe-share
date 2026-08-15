using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Users.UpdateUser;

public record UpdateUserCommand(Guid Id, string Name, string PublicKey, string EncryptedPrivateKey);

public class UpdateUserHandler(IUserRepository repo)
{
    public async Task<UserResponse> HandleAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await repo.GetAsync(command.Id, cancellationToken);
        if (user == null)
            throw new InvalidOperationException("User not found");
        
        await repo.UpdateAsync(command.Id, user);

        return new UserResponse(user.Id, user.Name, user.PublicKey, user.EncryptedPrivateKey);
    }
}