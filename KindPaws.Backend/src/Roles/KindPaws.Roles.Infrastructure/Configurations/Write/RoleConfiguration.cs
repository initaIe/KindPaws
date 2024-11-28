using KindPaws.Core.Extensions;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;
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
                value => RoleId.Create(value).Value)
            .HasColumnName("id");

        // ROLE_NAME
        builder.Property(r => r.Name)
            .HasConversion(
                name => name.Value,
                value => RoleName.Create(value).Value)
            .HasColumnType("citext")
            .HasColumnName("name")
            .IsRequired();
        builder.HasIndex(r => r.Name);

        // CREATION_TIMESTAMP
        builder.Property(r => r.CreationTimestamp)
            .HasColumnName("creation_timestamp")
            .IsRequired();

        // ROLE_PERMISSIONS
        builder.Property(r=>r.Permissions)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("permissions")
            .IsRequired();
    }
}