namespace SafeShare.Application.Features.Files.UploadFile;

public record GenerateUploadUrlCommand(string FileName, string ContentType, Guid OwnerId);