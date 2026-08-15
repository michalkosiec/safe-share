using System.Security.Cryptography;
using SafeShare.Application.Common.Interfaces;

namespace SafeShare.Infrastructure.Identity;

public class Pbkdf2PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int HashIterations = 300000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;
    private const char Delimiter = ';';

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            HashIterations,
            HashAlgorithm,
            KeySize
        );
        
        return string .Join(Delimiter.ToString(), HashIterations, Convert.ToBase64String(salt), 
            Convert.ToBase64String(hash));
    }

    public bool Verify(string hash, string password)
    {
        var parts = hash.Split(Delimiter);
        if (parts.Length != 3) 
            return false;
        
        int iterations = int.Parse(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] originalHash = Convert.FromBase64String(parts[2]);
        
        byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            HashAlgorithm,
            KeySize
        );
        
        return CryptographicOperations.FixedTimeEquals(originalHash, hashToCompare);
    }
}