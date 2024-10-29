using KindPaws.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDTO>
{
    public void Configure(EntityTypeBuilder<BreedDTO> builder)
    {
        builder.ToTable("breeds");

        // ID
        builder.Property(breed => breed.Id)
            .HasColumnName("id");

        // NAME
        builder.Property(breed => breed.Name)
            .HasColumnName("name");

        // DESCRIPTION
        builder.Property(breed => breed.Description)
            .HasColumnName("description");
        
        // SPECIE ID
        builder.Property(breed => breed.SpecieId)
            .HasColumnName("specie_id");
    }
}