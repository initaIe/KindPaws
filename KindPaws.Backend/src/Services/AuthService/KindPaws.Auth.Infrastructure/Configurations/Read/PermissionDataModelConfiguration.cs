using KindPaws.Auth.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Read;

public class PermissionDataModelConfiguration : IEntityTypeConfiguration<PermissionDataModel>
{
    public void Configure(EntityTypeBuilder<PermissionDataModel> builder)
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

        // CODE
        builder.Property(r => r.Code)
            .HasColumnName("code");
    }
}