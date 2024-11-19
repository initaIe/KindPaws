using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // TABLE NAMING
        builder.ToTable("roles");
        
        // ID
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id");

        // NAME (ShortAlphabeticString)
        builder
            .Property(r => r.Name)
            .HasColumnType("citext")
            .HasColumnName("name");
        builder.HasIndex(r => r.Name);
        
        // RELATIONS
        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();
    }
}