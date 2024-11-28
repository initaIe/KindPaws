using KindPaws.Permissions.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Permissions.Infrastructure.Configurations.Read;

public class PermissionDtoConfiguration : IEntityTypeConfiguration<PermissionDataModel>
{
    public void Configure(EntityTypeBuilder<PermissionDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("permissions");

        // ID
        builder.Property(p => p.Id)
            .HasColumnName("id");

        // CODE
        builder.Property(p => p.Code)
            .HasColumnName("code");

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");
    }
}