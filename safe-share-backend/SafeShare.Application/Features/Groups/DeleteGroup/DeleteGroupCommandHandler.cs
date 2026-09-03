using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Groups.DeleteGroup;

public class DeleteGroupCommandHandler(IGroupRepository repo)
{
    public async Task HandleAsync(DeleteGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await repo.GetAsync(command.Id, cancellationToken);
        if (group == null)
            throw new InvalidOperationException($"Group with id {command.Id} not found");
        await repo.DeleteAsync(command.Id, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);
    }
}