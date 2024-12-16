using KindPaws.Core.Extensions;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Roles.Infrastructure.Configurations.Write;

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
                value => UserRoleId.Create(value).Value)
            .HasColumnName("id");

        // ROLE_NAME
        builder.Property(r => r.Name)
            .HasConversion(
                name => name.Value,
                value => RoleName.Create(value).Value)
            .HasColumnType("citext")
            .HasColumnName("name")
            .IsRequired();

        // CREATED_AT
        builder.Property(r => r.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // ROLE_PERMISSIONS
        builder.Property(r => r.Permissions)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("permissions")
            .IsRequired();
    }
}