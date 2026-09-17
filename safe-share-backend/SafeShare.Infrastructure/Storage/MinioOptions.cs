namespace SafeShare.Infrastructure.Storage;

public sealed class MinioOptions
{
    public const string SectionName = "Minio";

    public string InternalEndpoint { get; init; } = "https://minio:9000";

    public string PublicEndpoint { get; init; } = "https://localhost:9000";

    public string AccessKey { get; init; } = "admin";

    public string SecretKey { get; init; } = "SuperSecret123!";

    public string CertificatePath { get; init; } = "/etc/minio/certs/public.crt";
}
