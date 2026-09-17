using Microsoft.EntityFrameworkCore;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Infrastructure.Persistence.Repositories;

public class GroupRepository(AppDbContext dbContext): IGroupRepository
{
    public async Task<Group?> GetAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id && x.OwnerId == userId, cancellationToken);
    }

    public async Task<IEnumerable<Group>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Groups.Where(x => x.OwnerId == userId).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(Group group, CancellationToken cancellationToken)
    {
        await dbContext.Groups.AddAsync(group, cancellationToken);
    }

    public Task UpdateAsync(Guid id, Guid userId, Group group)
    {
        if(group.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot update a group that is not the owner of this group");
        dbContext.Groups.Update(group);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var group = await GetAsync(id, userId, cancellationToken);
        if  (group == null)
            throw new KeyNotFoundException($"Group with id {id} not found");
        
        dbContext.Groups.Remove(group);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}