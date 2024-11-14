using KindPaws.Accounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        // ID
        builder.HasKey(p => p.Id);

        // CODE
        builder.Property(p => p.Code)
            .HasColumnName("code")
            .HasColumnType("citext")
            .IsRequired();

        builder.HasIndex(p => p.Code).IsUnique();
    }
}