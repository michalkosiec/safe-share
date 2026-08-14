using Microsoft.EntityFrameworkCore;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext): IUserRepository
{
    public async Task<User?> GetAsync(Guid id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public Task UpdateAsync(Guid id, User user)
    {
        dbContext.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetAsync(id);
        if  (user == null)
            throw new KeyNotFoundException($"User with id {id} not found");
        
        dbContext.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
