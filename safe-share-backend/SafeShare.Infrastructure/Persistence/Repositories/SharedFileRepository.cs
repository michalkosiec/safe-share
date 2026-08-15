using Microsoft.EntityFrameworkCore;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Infrastructure.Persistence.Repositories;

public class SharedFileRepository(AppDbContext dbContext): ISharedFileRepository
{
    public async Task<SharedFile?> GetAsync(Guid id)
    {
        return await dbContext.SharedFiles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<SharedFile>> GetAllAsync()
    {
        return await dbContext.SharedFiles.ToListAsync();
    }

    public async Task CreateAsync(SharedFile sharedFile)
    {
        await dbContext.SharedFiles.AddAsync(sharedFile);
    }

    public Task UpdateAsync(Guid id, SharedFile sharedFile)
    {
        dbContext.SharedFiles.Update(sharedFile);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var sharedFile = await GetAsync(id);
        if  (sharedFile == null)
            throw new KeyNotFoundException($"FIle with id {id} not found");

        dbContext.SharedFiles.Remove(sharedFile);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
