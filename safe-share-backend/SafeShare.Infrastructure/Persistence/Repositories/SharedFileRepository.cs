using Microsoft.EntityFrameworkCore;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Infrastructure.Persistence.Repositories;

public class SharedFileRepository(AppDbContext dbContext): ISharedFileRepository
{
    public async Task<SharedFile?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.SharedFiles.FirstOrDefaultAsync(x => x.Id == id,  cancellationToken);
    }

    public async Task<IEnumerable<SharedFile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SharedFiles.ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(SharedFile sharedFile, CancellationToken cancellationToken)
    {
        await dbContext.SharedFiles.AddAsync(sharedFile, cancellationToken);
    }

    public Task UpdateAsync(Guid id, SharedFile sharedFile)
    {
        dbContext.SharedFiles.Update(sharedFile);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sharedFile = await GetAsync(id, cancellationToken);
        if  (sharedFile == null)
            throw new KeyNotFoundException($"FIle with id {id} not found");

        dbContext.SharedFiles.Remove(sharedFile);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
