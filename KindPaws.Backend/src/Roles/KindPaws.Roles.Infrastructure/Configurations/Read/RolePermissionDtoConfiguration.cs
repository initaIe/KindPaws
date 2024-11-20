using KindPaws.Roles.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Roles.Infrastructure.Configurations.Read;

public class RolePermissionDtoConfiguration : IEntityTypeConfiguration<RolePermissionDto>
{
    public void Configure(EntityTypeBuilder<RolePermissionDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("role_permissions");

        // ROLE_ID
        builder.Property(rp => rp.RoleId)
            .HasColumnName("role_id");

        // PERMISSION_ID
        builder.Property(rp => rp.PermissionId)
            .HasColumnName("permission_id");

        // CREATION_TIMESTAMP
        builder.Property(rp => rp.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}