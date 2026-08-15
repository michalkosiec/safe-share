using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Users.DeleteUser;

public record DeleteUserCommand(Guid Id);

public class DeleteUserHandler(IUserRepository repo)
{
    public async Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await repo.GetAsync(command.Id, cancellationToken);
        if (user == null)
            throw new InvalidOperationException($"User with id {command.Id} not found");
        
        await repo.DeleteAsync(command.Id, cancellationToken);
    }
}