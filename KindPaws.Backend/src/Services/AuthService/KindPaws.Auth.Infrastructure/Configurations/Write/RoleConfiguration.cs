using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
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
        builder.HasKey(role => role.Id);
        builder.Property(role => role.Id)
            .HasConversion(
                id => id.Value,
                value => AccountRoleId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(role => role.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(role => role.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // NAME
        builder.Property(role => role.Name)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => RoleName.Create(value).Value)
            .HasMaxLength(RoleNameConstraints.MaxLength)
            .HasColumnType("varchar")
            .HasColumnName("name")
            .IsRequired();
        builder.HasIndex(r => r.Name).IsUnique();

        // PERMISSIONS
        builder.Property(role => role.Permissions)
            .HasUuidArrayConversion(
                id => id.Value,
                guid => PermissionId.Create(guid).Value)
            .HasColumnName("permissions");

        // IGNORE
        builder.Ignore(role => role.DomainEvents);
    }
}