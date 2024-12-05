using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Application.DataModels;
using KindPaws.Users.Domain.RolesManagement.AggregateRoot;
using KindPaws.Users.Domain.RolesManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Read;

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
        builder.Property(u => u.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // NAME
        builder.Property(r => r.Name)
            .HasColumnName("name");
    }
}