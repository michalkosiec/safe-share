using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken =  default);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken =  default);
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, User user);
    Task DeleteAsync(Guid id,  CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
