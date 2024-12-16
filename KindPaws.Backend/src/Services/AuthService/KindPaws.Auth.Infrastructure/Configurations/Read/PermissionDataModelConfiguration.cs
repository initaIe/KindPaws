using KindPaws.Auth.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Read;

public class PermissionDataModelConfiguration : IEntityTypeConfiguration<PermissionDataModel>
{
    public void Configure(EntityTypeBuilder<PermissionDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("permissions");

        // ID
        builder.Property(permission => permission.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(permission => permission.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(permission => permission.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // CODE
        builder.Property(permission => permission.Code)
            .HasColumnName("code");
    }
}