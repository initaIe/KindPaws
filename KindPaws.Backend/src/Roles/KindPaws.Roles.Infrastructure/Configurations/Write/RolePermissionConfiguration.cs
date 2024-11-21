using KindPaws.Roles.Domain.Entities;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Roles.Infrastructure.Configurations.Write;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        // TABLE NAMING
        builder.ToTable("role_permissions");

        // ID
        builder.Property(rp => rp.Id)
            .HasConversion(
                id => id.Value,
                value => RolePermissionId.Create(value).Value)
            .HasColumnName("id");

        // PERMISSION_ID
        builder.Property(rp => rp.PermissionId)
            .HasConversion(
                permissionId => permissionId.Value,
                value => PermissionId.Create(value).Value)
            .HasColumnName("permission_id")
            .IsRequired();

        // CREATION_TIMESTAMP
        builder.Property(rp => rp.CreationTimestamp)
            .HasColumnName("creation_timestamp")
            .IsRequired();
    }
}