using KindPaws.Roles.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Roles.Infrastructure.Configurations.Read;

public class RoleDtoConfiguration : IEntityTypeConfiguration<RoleDto>
{
    public void Configure(EntityTypeBuilder<RoleDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");

        // ID
        builder.Property(r => r.Id)
            .HasColumnName("id");

        // ROLE_NAME
        builder.Property(r => r.Name)
            .HasColumnName("name");

        // CREATION_TIMESTAMP
        builder.Property(r => r.CreationTimestamp)
            .HasColumnName("creation_timestamp");

        // ROLE_PERMISSIONS
        builder.HasMany(r => r.RolePermissions)
            .WithOne()
            .HasForeignKey(r => r.RoleId);
    }
}