using Microsoft.EntityFrameworkCore;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Infrastructure.Persistence.Repositories;

public class GroupRepository(AppDbContext dbContext): IGroupRepository
{
    public async Task<Group?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id,  cancellationToken);
    }

    public async Task<IEnumerable<Group>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Groups.ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(Group group, CancellationToken cancellationToken)
    {
        await dbContext.Groups.AddAsync(group, cancellationToken);
    }

    public Task UpdateAsync(Guid id, Group group)
    {
        dbContext.Groups.Update(group);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var group = await GetAsync(id, cancellationToken);
        if  (group == null)
            throw new KeyNotFoundException($"Group with id {id} not found");
        
        dbContext.Groups.Remove(group);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}