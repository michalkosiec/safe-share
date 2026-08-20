using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.Extensions.DependencyInjection;

namespace SafeShare.Infrastructure.Storage;

public static class StorageInitializer
{
    private const string BucketName = "safeshare-files";
    public static async Task InitializeAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var s3Client = scope.ServiceProvider.GetRequiredService<IAmazonS3>();
        
        try
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, BucketName);
            if (!bucketExists)
            {
                await s3Client.PutBucketAsync(BucketName);
                Console.WriteLine($"[Infrastructure] Bucket {BucketName} was successfully created.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Infrastructure] An error occured while creating the bucket: {ex.Message}.");
        }
    }
}