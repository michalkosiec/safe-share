using SafeShare.Domain.Entities;

namespace SafeShare.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task CreateAsync(User user);
    Task UpdateAsync(Guid id, User user);
    Task DeleteAsync(Guid id);
}