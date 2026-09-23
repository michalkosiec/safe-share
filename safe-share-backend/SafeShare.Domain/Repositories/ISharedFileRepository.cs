using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface ISharedFileRepository
{
    Task<SharedFile?> GetAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<SharedFile>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task CreateAsync(SharedFile sharedFile,  CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
