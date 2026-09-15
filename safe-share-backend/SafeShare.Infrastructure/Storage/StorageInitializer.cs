using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SafeShare.Infrastructure.Storage;

public static class StorageInitializer
{
    public static async Task InitializeAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var s3Client = scope.ServiceProvider.GetRequiredKeyedService<IAmazonS3>(S3ClientKeys.Internal);
        var options = scope.ServiceProvider.GetRequiredService<IOptions<S3StorageOptions>>().Value;
        
        try
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, options.BucketName);
            if (!bucketExists)
            {
                await s3Client.PutBucketAsync(options.BucketName);
                Console.WriteLine($"[Infrastructure] Bucket {options.BucketName} was successfully created.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Infrastructure] An error occured while creating the bucket: {ex.Message}.");
        }
    }
}