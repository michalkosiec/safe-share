using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Domain.Repositories;
using SafeShare.Infrastructure.Authentication;
using SafeShare.Infrastructure.Identity;
using SafeShare.Infrastructure.Persistence;
using SafeShare.Infrastructure.Storage;

namespace SafeShare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<S3StorageOptions>().Bind(configuration.GetSection("S3Storage")).ValidateDataAnnotations().ValidateOnStart();
        
        services.AddKeyedSingleton<IAmazonS3>(S3ClientKeys.Internal, (sp, _) =>
        {
            var options = sp.GetRequiredService<IOptions<S3StorageOptions>>().Value;
            var s3Config = new AmazonS3Config
            {
                ServiceURL = options.InternalServiceUrl, 
                ForcePathStyle = true 
            };
            
            return new AmazonS3Client(options.AccessKey, options.SecretKey, s3Config);
        });
        
        services.AddKeyedSingleton<IAmazonS3>(S3ClientKeys.Presign, (sp, _) =>
        {
            var options = sp.GetRequiredService<IOptions<S3StorageOptions>>().Value;
            var s3Config = new AmazonS3Config
            {
                ServiceURL = options.PublicServiceUrl, 
                ForcePathStyle = true 
            };
            
            return new AmazonS3Client(options.AccessKey, options.SecretKey, s3Config);
        });
        
        services.AddScoped<IFileStorageService, S3FileStorageService>();
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .AddScoped<Domain.Repositories.ISharedFileRepository,
                Persistence.Repositories.SharedFileRepository>();

        services.AddScoped<IUserRepository, Persistence.Repositories.UserRepository>();
        
        services.AddScoped<IGroupRepository, Persistence.Repositories.GroupRepository>();
        
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
