namespace SafeShare.Application.Features.Files.GenerateUploadUrl;

public record GenerateUploadUrlCommand(string FileName, string ContentType, Guid OwnerId);