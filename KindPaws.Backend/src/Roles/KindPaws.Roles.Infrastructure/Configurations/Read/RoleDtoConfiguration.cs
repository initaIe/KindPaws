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

        // CREATION_TIMESTAMP
        builder.Property(r => r.CreationTimestamp)
            .HasColumnName("creation_timestamp");

        // PERMISSIONS
        builder.Property(r => r.Permissions)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("permissions")
            .IsRequired();
    }
}