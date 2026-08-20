using Amazon.S3;
using Amazon.S3.Model;
using SafeShare.Application.Common.Interfaces;

namespace SafeShare.Infrastructure.Storage;

public class S3FileStorageService(IAmazonS3 s3client) :  IFileStorageService
{
    private readonly string _bucketName = "safeshare-files";
    public Task<string> GenerateUploadSignedUrlAsync(string fileId, TimeSpan expiresIn, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileId,
            Verb = HttpVerb.PUT,
            Expires = DateTime.Now.Add(expiresIn)
        };
        
        var url =  s3client.GetPreSignedURL(request);
        return Task.FromResult(url);
    }

    public Task<string> GenerateDownloadSignedUrlAsync(string fileId, TimeSpan expiresIn, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileId,
            Verb = HttpVerb.GET,
            Expires = DateTime.Now.Add(expiresIn)
        };
        
        var url =  s3client.GetPreSignedURL(request);
        return Task.FromResult(url);
    }

    public async Task DeleteFileAsync(string fileId, CancellationToken cancellationToken)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileId,
        };
        
        await s3client.DeleteObjectAsync(request, cancellationToken);
    }
}