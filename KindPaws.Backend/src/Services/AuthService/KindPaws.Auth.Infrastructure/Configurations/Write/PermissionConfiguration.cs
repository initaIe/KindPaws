using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Write;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // TABLE NAMING
        builder.ToTable("permissions");

        // ID
        builder.HasKey(permission => permission.Id);
        builder.Property(permission => permission.Id)
            .HasConversion(
                id => id.Value,
                value => PermissionId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(permission => permission.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(permission => permission.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // CODE
        builder.Property(permission => permission.Code)
            .HasConversion(
                code => code!.Value,
                value => PermissionCode.Create(value).Value)
            .HasMaxLength(PermissionCodeConstraints.MaxLength)
            .HasColumnType("varchar")
            .HasColumnName("code")
            .IsRequired();
        builder.HasIndex(p => p.Code).IsUnique();


        // IGNORE
        builder.Ignore(permission => permission.DomainEvents);
    }
}