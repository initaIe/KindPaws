using KindPaws.Permissions.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Permissions.Infrastructure.Configurations.Read;

public class PermissionDtoConfiguration : IEntityTypeConfiguration<PermissionDto>
{
    public void Configure(EntityTypeBuilder<PermissionDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("permissions");

        // ID
        builder.Property(p => p.Id)
            .HasColumnName("id");

        // CODE
        builder.Property(p => p.Code)
            .HasColumnName("code");

        // CREATION_TIMESTAMP
        builder.Property(p => p.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}