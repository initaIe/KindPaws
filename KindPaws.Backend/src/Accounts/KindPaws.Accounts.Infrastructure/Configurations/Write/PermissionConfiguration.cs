using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

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
            .HasColumnName("code")
            .HasColumnType("citext")
            .IsRequired();
        builder.HasIndex(p => p.Code).IsUnique();
    }
}