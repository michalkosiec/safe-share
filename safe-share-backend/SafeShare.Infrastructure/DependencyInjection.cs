using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Infrastructure.Authentication;
using SafeShare.Infrastructure.Identity;
using SafeShare.Infrastructure.Persistence;
using SafeShare.Infrastructure.Storage;

namespace SafeShare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var s3Config = new AmazonS3Config
        {
            ServiceURL = "http://minio:9000", 
            ForcePathStyle = true 
        };
        
        services.AddSingleton<IAmazonS3>(new AmazonS3Client("admin", "SuperSecret123!", s3Config));
        
        services.AddScoped<IFileStorageService, S3FileStorageService>();
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddScoped<SafeShare.Domain.Repositories.IUserRepository, SafeShare.Infrastructure.Persistence.Repositories.UserRepository>();

        services
            .AddScoped<SafeShare.Domain.Repositories.ISharedFileRepository,
                SafeShare.Infrastructure.Persistence.Repositories.SharedFileRepository>();
        
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}