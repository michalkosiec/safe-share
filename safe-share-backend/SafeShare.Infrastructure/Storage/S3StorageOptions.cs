using System.ComponentModel.DataAnnotations;

namespace SafeShare.Infrastructure.Storage;

public class S3StorageOptions
{
    [Required]
    public string BucketName { get; set; } = string.Empty;
    [Required]
    public string InternalServiceUrl { get; set; } = string.Empty;
    [Required]
    public string PublicServiceUrl { get; set; } = string.Empty;
    [Required]
    public string AccessKey { get; set; } = string.Empty;
    [Required]
    public string SecretKey { get; set; } = string.Empty;
}