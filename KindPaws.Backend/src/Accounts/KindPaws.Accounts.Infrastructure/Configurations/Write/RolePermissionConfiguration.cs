using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        // TABLE NAMING
        builder.ToTable("role_permissions");
        
        // ID
        builder.HasKey(rp => rp.Id);
        builder.Property(rp => rp.Id)
            .HasConversion(
                id => id.Value,
                value => RolePermissionId.Create(value).Value)
            .HasColumnName("id");

        // RELATIONS
        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);
        builder.HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
        
        // ROLE ID
        builder.Property(rp => rp.RoleId)
            .HasColumnName("role_id");
        
        // PERMISSION_ID
        builder.Property(rp => rp.PermissionId)
            .HasConversion(
                id => id.Value,
                value => PermissionId.Create(value).Value)
            .HasColumnName("permission_id");
    }
}