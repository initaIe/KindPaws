using KindPaws.Domain.Managements.SpecieManagement.Constraints;
using KindPaws.Domain.Managements.SpecieManagement.Entities;
using KindPaws.Domain.Shared.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(breed => breed.Id);

        builder.Property(breed => breed.Id)
            .HasConversion(
                breedId => breedId.Value,
                value => BreedId.Create(value))
            .HasColumnName("id");

        builder.Property(breed => breed.Name)
            .HasMaxLength(BreedConstraints.MaxNameLength)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(breed => breed.Description)
            .HasMaxLength(BreedConstraints.MaxDescriptionLength)
            .HasColumnName("description")
            .IsRequired();

        builder.OwnsOne(breed => breed.ColorList, colorList =>
        {
            colorList.ToJson("colors");
            colorList.OwnsMany(x => x.BreedColors)
                .Property(x => x.Value);
        });
    }
}