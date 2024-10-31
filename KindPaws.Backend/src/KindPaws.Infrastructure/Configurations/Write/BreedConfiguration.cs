using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations.Write;

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
                value => BreedId.Create(value).Value)
            .HasColumnName("id");

        // NAME
        builder.ComplexProperty(breed => breed.Name, name =>
        {
            name.Property(x => x.Value)
                .HasMaxLength(ShortNameConstraints.MaxLength)
                .HasColumnName("name")
                .HasColumnType("citext")
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

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT DELETE DATE TIME
        builder.Property(b => b.SoftDeletedDateTime)
            .HasColumnName("soft_delete_datetime")
            .IsRequired(false);

        // HARD DELETE PROPERTY IGNORE
        builder.Ignore(b => b.IsHardDeleted);
    }
}