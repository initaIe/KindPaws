using KindPaws.Domain.Managements.BreedManagement.AggregateRoot;
using KindPaws.Domain.Managements.BreedManagement.Constraints;
using KindPaws.Domain.Shared.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.HasKey(breed => breed.Id);

        builder.Property(breed => breed.Id)
            .HasConversion(
                breedId => breedId.Value,
                value => BreedId.Create(value));

        builder.Property(breed => breed.Name)
            .HasMaxLength(BreedConstraints.MaxNameLength)
            .IsRequired();

        builder.Property(breed => breed.Description)
            .HasMaxLength(BreedConstraints.MaxDescriptionLength)
            .IsRequired();

        builder.HasOne(breed => breed.Specie)
            .WithMany(specie => specie.Breeds)
            .HasForeignKey(breed => breed.SpecieId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(breed => breed.ColorList, colorList =>
        {
            colorList.ToJson("colors");
            colorList.OwnsMany(x => x.BreedColors)
                .Property(x => x.Value);
        });
    }
}