using System.Security.Cryptography.X509Certificates;
using Amazon.Runtime;

namespace SafeShare.Infrastructure.Storage;

public sealed class MinioHttpClientFactory : HttpClientFactory
{
    private readonly X509Certificate2? _certificate;

    public MinioHttpClientFactory(string certificatePath)
    {
        if (File.Exists(certificatePath))
        {
            _certificate = X509CertificateLoader.LoadCertificateFromFile(certificatePath);
        }
    }

    public override HttpClient CreateHttpClient(IClientConfig clientConfig)
    {
        var handler = new HttpClientHandler();
        if (_certificate is not null)
        {
            handler.ServerCertificateCustomValidationCallback = (_, certificate, _, _) =>
                certificate is not null &&
                string.Equals(
                    certificate.GetCertHashString(),
                    _certificate.GetCertHashString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        return new HttpClient(handler);
    }
}
