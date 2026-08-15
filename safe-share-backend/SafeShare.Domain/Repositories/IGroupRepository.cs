using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface IGroupRepository
{
    Task<Group?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Group>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(Group group, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Group group);
    Task DeleteAsync(Guid id,  CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
