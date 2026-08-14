using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface ISharedFileRepository
{
    Task<SharedFile?> GetAsync(Guid id);
    Task<IEnumerable<SharedFile>> GetAllAsync();
    Task CreateAsync(SharedFile sharedFile);
    Task UpdateAsync(Guid id, SharedFile sharedFile);
    Task DeleteAsync(Guid id);
}