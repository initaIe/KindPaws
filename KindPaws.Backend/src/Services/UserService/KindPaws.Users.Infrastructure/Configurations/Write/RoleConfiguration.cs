using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.RolesManagement.AggregateRoot;
using KindPaws.Users.Domain.RolesManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.RolesManagement.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Write;

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
                value => RoleId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(u => u.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // NAME
        builder.Property(r => r.Name)
            .HasConversion(
                name => name.Value,
                value => RoleName.Create(value).Value)
            .HasMaxLength(RoleNameConstraints.MaxLength)
            .HasColumnName("name")
            .IsRequired();

        // IGNORE
        builder.Ignore(r => r.DomainEvents);
    }
}