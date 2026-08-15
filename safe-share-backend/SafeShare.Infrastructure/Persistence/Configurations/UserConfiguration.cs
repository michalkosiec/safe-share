using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeShare.Domain.Entities;

namespace SafeShare.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.PasswordHash).HasMaxLength(260).IsRequired();
        builder.Property(x => x.PublicKey).HasMaxLength(50).IsRequired();
        builder.Property(x => x.EncryptedPrivateKey).HasMaxLength(50).IsRequired();
    }
}