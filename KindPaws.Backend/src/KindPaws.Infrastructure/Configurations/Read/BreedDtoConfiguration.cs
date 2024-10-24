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
        builder.Property(breed => breed.Id);

        // NAME
        builder.Property(breed => breed.Name);

        // DESCRIPTION
        builder.Property(breed => breed.Description);
    }
}