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

        // ROLE_ID
        builder.Property(rp => rp.RoleId)
            .HasConversion(
                roleId => roleId.Value,
                value => RoleId.Create(value).Value)
            .HasColumnName("role_id")
            .IsRequired();

        // PERMISSION_ID
        builder.Property(rp => rp.PermissionId)
            .HasConversion(
                permissionId => permissionId.Value,
                value => PermissionId.Create(value).Value)
            .HasColumnName("permission_id")
            .IsRequired();

        // KEY
        builder.HasKey(rp => new
        {
            rp.RoleId, rp.PermissionId
        });
    }
}