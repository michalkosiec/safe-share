using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeShare.Domain.Entities;

namespace SafeShare.Infrastructure.Persistence.Configurations;

public class SharedFileConfiguration : IEntityTypeConfiguration<SharedFile>
{
    public void Configure(EntityTypeBuilder<SharedFile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OwnerId).IsRequired();
        builder.Property(x => x.FileName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ContentType).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
    }
}