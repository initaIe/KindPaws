using System.Text.Json;
using KindPaws.Auth.Application.DataModels;
using KindPaws.Auth.Application.Mappers;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Read;

public class RoleDataModelConfiguration : IEntityTypeConfiguration<RoleDataModel>
{
    public void Configure(EntityTypeBuilder<RoleDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");

        // ID
        builder.Property(r => r.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(r => r.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // NAME
        builder.Property(r => r.Name)
            .HasColumnName("name");

        // PERMISSIONS
        builder.Property(r => r.Permissions)
            .HasColumnName("permissions");
    }
}