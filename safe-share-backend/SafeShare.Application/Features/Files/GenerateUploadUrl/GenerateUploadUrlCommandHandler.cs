using SafeShare.Application.Common.Interfaces;
using SafeShare.Application.Features.Files.DTOs;
using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Files.GenerateUploadUrl;

public class GenerateUploadUrlCommandHandler(IFileStorageService fileStorageService, ISharedFileRepository repo)
{
    public async Task<GenerateUploadUrlResponse> HandleAsync(GenerateUploadUrlCommand command, CancellationToken cancellationToken)
    {
        var fileRecord = new SharedFile(command.UserId, command.FileName, command.ContentType);
        await repo.CreateAsync(fileRecord, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);

        var url = await fileStorageService.GenerateUploadSignedUrlAsync(
            fileRecord.Id.ToString(),
            command.ContentType,
            TimeSpan.FromMinutes(15),
            cancellationToken);
        
        return new GenerateUploadUrlResponse(url, fileRecord.Id);
    }
}