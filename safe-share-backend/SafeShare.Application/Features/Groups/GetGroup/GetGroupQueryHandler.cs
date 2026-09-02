using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Groups.GetGroup;

public class GetGroupQueryHandler(IGroupRepository repo)
{
    public async Task<GroupResponse> HandleAsync(GetGroupQuery query, CancellationToken cancellationToken)
    {
        var group = await repo.GetAsync(query.Id, cancellationToken);
        return group == null
            ? throw new InvalidOperationException($"Group with id {query.Id} not found.")
            : new GroupResponse(group.Id, group.Name, group.OwnerId);
    }
}