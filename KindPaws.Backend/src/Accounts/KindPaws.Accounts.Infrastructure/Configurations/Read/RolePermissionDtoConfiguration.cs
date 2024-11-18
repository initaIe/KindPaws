using KindPaws.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class RolePermissionDtoConfiguration : IEntityTypeConfiguration<RolePermissionDto>
{
    public void Configure(EntityTypeBuilder<RolePermissionDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("role_permissions");
        
        // ID
        builder.Property(rp => rp.Id)
            .HasColumnName("id");
        
        // ROLE ID
        builder.Property(rp => rp.RoleId)
            .HasColumnName("role_id");
        
        // PERMISSION_ID
        builder.Property(rp => rp.PermissionId)
            .HasColumnName("permission_id");
    }
}