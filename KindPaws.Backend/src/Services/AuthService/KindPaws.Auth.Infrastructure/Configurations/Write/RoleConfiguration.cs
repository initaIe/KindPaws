using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Write;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");

        // ID
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => AccountRoleId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(r => r.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // NAME
        builder.Property(r => r.Name)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => RoleName.Create(value).Value)
            .HasColumnName("name")
            .IsRequired();

        // PERMISSIONS
        builder.Property(r => r.Permissions)
            .HasUuidArrayConversion<PermissionId>(
                id => id.Value,
                guid => PermissionId.Create(guid).Value)
            .HasColumnName("permissions")
            .IsRequired();

        // IGNORE
        builder.Ignore(r => r.DomainEvents);
    }
}