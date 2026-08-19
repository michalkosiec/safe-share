namespace SafeShare.Application.Features.Files.GenerateDownloadUrl;

public record GenerateDownloadUrlCommand(Guid FileId, Guid OwnerId);
