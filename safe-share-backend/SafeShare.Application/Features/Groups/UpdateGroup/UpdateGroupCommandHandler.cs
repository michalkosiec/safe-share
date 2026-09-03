using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Groups.UpdateGroup;

public class UpdateGroupCommandHandler(IGroupRepository repo)
{
    public async Task<GroupResponse> Handle(UpdateGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await repo.GetAsync(command.Id, cancellationToken);
        if (group == null)
            throw new InvalidOperationException("Group not found");

        await repo.UpdateAsync(command.Id, group);
        await repo.SaveChangesAsync(cancellationToken);
        return new GroupResponse(group.Id, group.Name, group.OwnerId);
    }
}