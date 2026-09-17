namespace SafeShare.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PasswordHash { get; private set; }
        public string PublicKey { get; private set; }
        public string EncryptedPrivateKey { get; private set; }
        
        private User() {}
        
        public User(string name, string passwordHash, string publicKey, string encryptedPrivateKey)
        {
            Id = Guid.NewGuid();
            Name = name;
            PasswordHash = passwordHash;
            PublicKey = publicKey;
            EncryptedPrivateKey = encryptedPrivateKey;
        }

        public void UpdateUser(string name, string publicKey, string encryptedPrivateKey)
        {
            Name = name;
            PublicKey = publicKey;
            EncryptedPrivateKey = encryptedPrivateKey;
        }
        //probably add ChangeUserPassword later.
    }
}