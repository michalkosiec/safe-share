namespace SafeShare.Application.Features.Users.UpdateUser;

public record UpdateUserCommand(Guid Id, string Name, string PublicKey, string EncryptedPrivateKey);