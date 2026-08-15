using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Infrastructure.Identity;
using SafeShare.Infrastructure.Persistence;

namespace SafeShare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SafeShare.Domain.Repositories.IUserRepository, SafeShare.Infrastructure.Persistence.Repositories.UserRepository>();
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}