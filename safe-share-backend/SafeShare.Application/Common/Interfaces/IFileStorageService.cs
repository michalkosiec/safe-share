namespace SafeShare.Application.Common.Interfaces;

public interface IFileStorageService
{
    /// <summary>
    /// Generates a temporary link (PUT) which can be used to upload the file directly to the storage.
    /// </summary>
    /// <param name="fileId">The unique identifier of the file.</param>
    /// <param name="expiresIn">The duration for which the generated link will remain valid.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A pre-signed URL as a string.</returns>
    Task<string> GenerateUploadSignedUrlAsync(string fileId, TimeSpan expiresIn, CancellationToken cancellationToken);
    
    /// <summary>
    /// Generates a temporary link (GET) which can be used to securely download the file.
    /// </summary>
    /// <param name="fileId">The unique identifier of the file.</param>
    /// <param name="expiresIn">The duration for which the generated link will remain valid.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A pre-signed URL as a string.</returns>
    Task<string> GenerateDownloadSignedUrlAsync(string fileId, TimeSpan expiresIn, CancellationToken cancellationToken);
    
    /// <summary>
    /// Removes the specified file from the blob storage.
    /// </summary>
    /// <param name="fileId">The unique identifier of the file to remove.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    Task DeleteFileAsync(string fileId, CancellationToken cancellationToken);
}