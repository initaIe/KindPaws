using KindPaws.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class RoleDtoConfiguration : IEntityTypeConfiguration<RoleDto>
{
    public void Configure(EntityTypeBuilder<RoleDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");
        
        // ID
        builder.Property(x => x.Id)
            .HasColumnName("id");

        // NAME (ShortAlphabeticString)
        builder.Property(r => r.Name)
            .HasColumnName("name");
    }
}