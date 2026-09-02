using SafeShare.Application.Common.Interfaces;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Files.GenerateUploadUrl;

public class GenerateUploadUrlCommandHandler(IFileStorageService fileStorageService, ISharedFileRepository repo)
{
    public async Task<string> HandleAsync(GenerateUploadUrlCommand command, CancellationToken cancellationToken)
    {
        var fileRecord = new SharedFile(command.OwnerId, command.FileName, command.ContentType);
        await repo.CreateAsync(fileRecord, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);

        return await fileStorageService.GenerateUploadSignedUrlAsync(fileRecord.Id.ToString(), TimeSpan.FromMinutes(15), cancellationToken);
    }
}