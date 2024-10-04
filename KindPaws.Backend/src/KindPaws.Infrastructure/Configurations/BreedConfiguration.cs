using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        // ID
        builder.HasKey(breed => breed.Id);

        builder.Property(breed => breed.Id)
            .HasConversion(
                breedId => breedId.Value,
                value => BreedId.Create(value))
            .HasColumnName("id");

        // NAME
        builder.ComplexProperty(breed => breed.Name, name =>
        {
            name.Property(x => x.Value)
                .HasMaxLength(ShortNameConstraints.MaxLength)
                .HasColumnName("name")
                .IsRequired();
        });

        // DESCRIPTION
        builder.ComplexProperty(breed => breed.Description, description =>
        {
            description.Property(x => x.Value)
                .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                .HasColumnName("description")
                .IsRequired();
        });
    }
}