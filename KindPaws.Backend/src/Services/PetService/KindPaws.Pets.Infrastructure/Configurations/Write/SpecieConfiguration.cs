using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Write;

public class SpecieConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        // TABLE NAMING
        builder.ToTable("species");

        // ID
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SpecieId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(s => s.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(s => s.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // NAME
        builder.Property(s => s.Name)
            .HasConversion(
                name => name!.Value,
                value => SpecieName.Create(value).Value)
            .HasColumnType("citext")
            .HasColumnName("name")
            .IsRequired();

        // DESCRIPTION
        builder.Property(s => s.Description)
            .HasConversion(
                description => description!.Value,
                value => SpecieDescription.Create(value).Value)
            .HasMaxLength(SpecieDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired();

        // BREEDS
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("specie_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // IGNORE
        builder.Ignore(s => s.DomainEvents);
    }
}