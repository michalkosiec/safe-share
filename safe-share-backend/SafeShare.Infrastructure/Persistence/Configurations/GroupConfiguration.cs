using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeShare.Domain.Entities;

namespace SafeShare.Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Navigation(x => x.Users).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}