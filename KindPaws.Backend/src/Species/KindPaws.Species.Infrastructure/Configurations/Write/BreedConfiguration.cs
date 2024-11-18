using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;
using KindPaws.Species.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Species.Infrastructure.Configurations.Write;

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
        builder.Property(breed => breed.Name)
            .HasConversion(
                name => name.Value,
                value => ShortAlphabeticWhiteSpacesString.Create(value).Value)
            .HasMaxLength(ShortAlphabeticStringConstraints.MaxLength)
            .HasColumnName("name")
            .HasColumnType("citext")
            .IsRequired();
        builder.HasIndex(b => b.Name).IsUnique();

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
        builder.Property(breed => breed.SoftDeletionTimestamp)
            .HasConversion(
                utc => utc!.Value,
                date => UtcNowTimestamp.Create(date))
            .HasColumnName("soft_delete_datetime")
            .IsRequired(false);
    }
}