using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Permissions.Infrastructure.Configurations.Write;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // TABLE NAMING
        builder.ToTable("permissions");

        // ID
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PermissionId.Create(value).Value)
            .HasColumnName("id");

        // CODE
        builder.Property(p => p.Code)
            .HasConversion(
                permissionCode => permissionCode.Value,
                value => PermissionCode.Create(value).Value)
            .HasColumnName("code")
            .IsRequired();

        // CREATION_TIMESTAMP
        builder.Property(p => p.CreatedAt)
            .HasConversion(
                creationTimestamp => creationTimestamp.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("creation_timestamp")
            .IsRequired();
    }
}