using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface IGroupRepository
{
    Task<Group?> GetAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Group>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task CreateAsync(Group group, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Guid userId, Group group);
    Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
