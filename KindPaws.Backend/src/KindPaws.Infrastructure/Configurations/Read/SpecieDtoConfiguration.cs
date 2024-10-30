using KindPaws.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations.Read;

public class SpecieDtoConfiguration : IEntityTypeConfiguration<SpecieDTO>
{
    public void Configure(EntityTypeBuilder<SpecieDTO> builder)
    {
        builder.ToTable("species");

        // ID
        builder.Property(specie => specie.Id)
            .HasColumnName("id");

        // NAME
        builder.Property(specie => specie.Name)
            .HasColumnName("name")
            .HasColumnType("citext");

        // DESCRIPTION
        builder.Property(specie => specie.Description)
            .HasColumnName("description");

        // BREEDS
        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey(b => b.SpecieId);
    }
}