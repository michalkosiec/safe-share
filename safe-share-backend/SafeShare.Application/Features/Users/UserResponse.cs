namespace SafeShare.Application.Features.Users;

public record UserResponse(Guid Id, string Name, string PublicKey, string EncryptedPrivateKey);