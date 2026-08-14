using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface IGroupRepository
{
    Task<Group?> GetAsync(Guid id);
    Task<IEnumerable<Group>> GetAllAsync();
    Task CreateAsync(Group group);
    Task UpdateAsync(Guid id, Group group);
    Task DeleteAsync(Guid id);
}