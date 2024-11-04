using KindPaws.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Species.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breeds");

        // ID
        builder.Property(breed => breed.Id)
            .HasColumnName("id");

        // NAME
        builder.Property(breed => breed.Name)
            .HasColumnName("name")
            .HasColumnType("citext");

        // DESCRIPTION
        builder.Property(breed => breed.Description)
            .HasColumnName("description");

        // SPECIE ID
        builder.Property(breed => breed.SpecieId)
            .HasColumnName("specie_id");
    }
}