using KindPaws.Core.Extensions;
using KindPaws.Roles.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Roles.Infrastructure.Configurations.Read;

public class RoleDtoConfiguration : IEntityTypeConfiguration<RoleDataModel>
{
    public void Configure(EntityTypeBuilder<RoleDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");

        // ID
        builder.Property(r => r.Id)
            .HasColumnName("id");

        // ROLE_NAME
        builder.Property(r => r.Name)
            .HasColumnName("name");

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at");

        // PERMISSIONS
        builder.Property(r => r.Permissions)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("permissions")
            .IsRequired();
    }
}