using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Species.Domain.Entities;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;
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
                value => BreedName.Create(value).Value)
            .HasColumnName("name")
            .HasColumnType("citext")
            .IsRequired();

        // DESCRIPTION
        builder.Property(b => b.Description)
            .HasConversion(
                description => description.Value,
                value => BreedDescription.Create(value).Value)
            .HasMaxLength(BreedDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired();

        // CREATED_AT
        builder.Property(breed => breed.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT_DELETED_AT
        builder.Property(breed => breed.SoftDeletedAt)
            .HasColumnName("soft_deleted_at")
            .IsRequired(false);
    }
}