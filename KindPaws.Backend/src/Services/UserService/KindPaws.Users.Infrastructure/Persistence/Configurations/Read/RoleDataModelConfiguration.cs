using KindPaws.Users.Application.Common.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Read;

public class RoleDataModelConfiguration : IEntityTypeConfiguration<RoleDataModel>
{
    public void Configure(EntityTypeBuilder<RoleDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");

        // ID
        builder.Property(role => role.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(role => role.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(role => role.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // NAME
        builder.Property(role => role.Name)
            .HasColumnName("name");
    }
}