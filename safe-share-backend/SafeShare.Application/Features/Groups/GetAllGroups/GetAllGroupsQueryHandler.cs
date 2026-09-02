using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Groups.GetAllGroups;

public class GetAllGroupsHandler(IGroupRepository repo)
{
    public async Task<IEnumerable<GroupResponse>> HandleAsync(GetAllGroupsQuery query,
        CancellationToken cancellationToken)
    {
        var groups = await repo.GetAllAsync(cancellationToken);
        return groups.Select(x => new GroupResponse(x.Id, x.Name, x.OwnerId));
    }
}