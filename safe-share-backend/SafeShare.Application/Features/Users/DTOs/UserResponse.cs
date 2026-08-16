namespace SafeShare.Application.Features.Users.DTOs;

public record UserResponse(Guid Id, string Name, string PublicKey, string EncryptedPrivateKey);