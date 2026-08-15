using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface ISharedFileRepository
{
    Task<SharedFile?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SharedFile>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(SharedFile sharedFile,  CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, SharedFile sharedFile);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
