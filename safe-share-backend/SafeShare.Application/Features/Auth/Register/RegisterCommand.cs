namespace SafeShare.Application.Features.Auth.Register;

public record RegisterCommand(string UserName, string Password, string PublicKey, string EncryptedPrivateKey);