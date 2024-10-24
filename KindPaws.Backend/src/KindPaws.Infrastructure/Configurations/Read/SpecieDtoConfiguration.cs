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
        builder.Property(specie => specie.Id);

        // NAME
        builder.Property(specie => specie.Name);

        // DESCRIPTION
        builder.Property(specie => specie.Description);
        
        // BREEDS
        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey("specie_id");
    }
}