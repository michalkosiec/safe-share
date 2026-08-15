using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Users;

public record GetAllUsersQuery();

public class GetAllUsersHandler(IUserRepository repo)
{
    public async Task<IEnumerable<UserResponse>> HandleAsync(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await repo.GetAllAsync(cancellationToken);
        
        return users.Select(u => new UserResponse(u.Id, u.Name, u.PublicKey, u.EncryptedPrivateKey));
    }
}