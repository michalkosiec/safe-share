namespace SafeShare.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PasswordHash { get; private set; }
        public string PublicKey { get; private set; }
        public string EncryptedPrivateKey { get; private set; }
        
        public User(string name, string passwordHash, string publicKey, string encryptedPrivateKey)
        {
            Id = Guid.NewGuid();
            Name = name;
            PasswordHash = passwordHash;
            PublicKey = publicKey;
            EncryptedPrivateKey = encryptedPrivateKey;
        }
    }
}