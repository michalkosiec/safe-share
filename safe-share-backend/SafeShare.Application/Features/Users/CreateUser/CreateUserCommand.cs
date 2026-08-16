namespace SafeShare.Application.Features.Users.CreateUser;

public record CreateUserCommand(string Name, string Password, string PublicKey, string EncryptedPrivateKey);
