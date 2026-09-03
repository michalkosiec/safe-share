using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Groups.CreateGroup;

public class CreateGroupCommandHandler(IGroupRepository repo)
{
    public async Task<GroupResponse> HandleAsync(CreateGroupCommand command, CancellationToken cancellationToken) //make it return GroupResponse
    {
        var group = new Group(command.Name, command.OwnerId);
        
        await repo.CreateAsync(group, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);
        
        var createdGroup = await repo.GetAsync(group.Id, cancellationToken);
        if (createdGroup == null)
            throw new InvalidOperationException("Group not created");
        
        var groupResponse = new GroupResponse(createdGroup.Id, createdGroup.Name, createdGroup.OwnerId);

        return groupResponse;
    }
}