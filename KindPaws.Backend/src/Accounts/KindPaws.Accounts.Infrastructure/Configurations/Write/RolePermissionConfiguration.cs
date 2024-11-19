using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Role;
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
        
        // KEY
        builder.HasKey(rp=> new { rp.RoleId, rp.PermissionId });
        
        // CREATION TIMESTAMP
        builder.Property(ar => ar.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}