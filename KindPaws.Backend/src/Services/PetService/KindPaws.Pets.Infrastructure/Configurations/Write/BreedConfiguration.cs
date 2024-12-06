using KindPaws.Pets.Domain.SpeciesManagement.Entities;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        // TABLE NAMING
        builder.ToTable("breeds");

        // ID
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(b => b.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(b => b.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // NAME
        builder.Property(s => s.Name)
            .HasConversion(
                name => name!.Value,
                value => BreedName.Create(value).Value)
            .HasColumnType("citext")
            .HasColumnName("name")
            .IsRequired();

        // DESCRIPTION
        builder.Property(s => s.Description)
            .HasConversion(
                description => description!.Value,
                value => BreedDescription.Create(value).Value)
            .HasMaxLength(BreedDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired();
    }
}