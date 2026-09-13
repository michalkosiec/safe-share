using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SafeShare.Application.Common.Interfaces;

namespace SafeShare.Infrastructure.Storage;

public class S3FileStorageService([FromKeyedServices(S3ClientKeys.Internal)] IAmazonS3 s3Client, [FromKeyedServices(S3ClientKeys.Presign)] IAmazonS3 presignClient, IOptions<S3StorageOptions> options) :  IFileStorageService
{
    private readonly string _bucketName = options.Value.BucketName;
    public Task<string> GenerateUploadSignedUrlAsync(string fileId, TimeSpan expiresIn, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileId,
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.Add(expiresIn)
        };
        
        var url =  presignClient.GetPreSignedURL(request);
        return Task.FromResult(url);
    }

    public Task<string> GenerateDownloadSignedUrlAsync(string fileId, TimeSpan expiresIn, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileId,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.Add(expiresIn)
        };
        
        var url =  presignClient.GetPreSignedURL(request);
        return Task.FromResult(url);
    }

    public async Task DeleteFileAsync(string fileId, CancellationToken cancellationToken)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileId,
        };
        
        await s3Client.DeleteObjectAsync(request, cancellationToken);
    }
}