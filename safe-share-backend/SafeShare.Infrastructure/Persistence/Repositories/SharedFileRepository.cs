using Microsoft.EntityFrameworkCore;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Infrastructure.Persistence.Repositories;

public class SharedFileRepository(AppDbContext dbContext): ISharedFileRepository
{
    public async Task<SharedFile?> GetAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.SharedFiles.FirstOrDefaultAsync(x => x.Id == id && x.OwnerId == userId,  cancellationToken);
    }

    public async Task<IEnumerable<SharedFile>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.SharedFiles.Where(x => x.OwnerId == userId).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(SharedFile sharedFile, CancellationToken cancellationToken)
    {
        await dbContext.SharedFiles.AddAsync(sharedFile, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var sharedFile = await GetAsync(id, userId, cancellationToken);
        if  (sharedFile == null)
            throw new KeyNotFoundException($"File with id {id} not found");

        dbContext.SharedFiles.Remove(sharedFile);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
