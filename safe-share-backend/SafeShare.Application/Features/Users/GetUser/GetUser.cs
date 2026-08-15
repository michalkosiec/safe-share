using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Users;

public record GetUserQuery(Guid Id);

public class GetUserHandler(IUserRepository repo)
{
    public async Task<UserResponse> HandleAsync(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await repo.GetAsync(query.Id, cancellationToken);
        return user == null ? throw new InvalidOperationException($"User with id {query.Id} not found") : new UserResponse(user.Id, user.Name, user.PublicKey, user.EncryptedPrivateKey);
    }
}