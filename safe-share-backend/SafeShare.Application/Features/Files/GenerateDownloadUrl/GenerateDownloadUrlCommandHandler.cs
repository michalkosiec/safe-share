using SafeShare.Application.Common.Interfaces;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Files.GenerateDownloadUrl;

public class GenerateDownloadUrlCommandHandler(IFileStorageService fileStorageService, ISharedFileRepository repo)
{
    public async Task<string> Handle(GenerateDownloadUrlCommand command, CancellationToken cancellationToken)
    {
        var fileRecord = await repo.GetAsync(command.FileId, cancellationToken);

        if (fileRecord == null)
            throw new Exception("File not found");

        if (fileRecord.OwnerId != command.OwnerId)
            throw new Exception("You do not have permission to download this file");
        
        if (fileRecord.Status != SharedFileStatus.Available) 
            throw new Exception("File is not ready to download.");
        
        var downloadLink = await fileStorageService.GenerateDownloadSignedUrlAsync(command.FileId.ToString(), TimeSpan.FromHours(1),  cancellationToken);
        
        return downloadLink;
    }
}