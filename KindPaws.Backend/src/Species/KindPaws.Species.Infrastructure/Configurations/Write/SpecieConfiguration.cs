using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Species.Infrastructure.Configurations.Write;

public class SpecieConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        builder.ToTable("species");

        // ID
        builder.HasKey(specie => specie.Id);
        builder.Property(specie => specie.Id)
            .HasConversion(
                specieId => specieId.Value,
                value => SpecieId.Create(value).Value)
            .HasColumnName("id");

        // BREEDS
        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey("specie_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // NAME
        builder.Property(specie => specie.Name)
            .HasConversion(
                name => name.Value,
                value => SpecieName.Create(value).Value)
            .HasColumnName("name")
            .HasColumnType("citext")
            .IsRequired();
        builder.HasIndex(s => s.Name).IsUnique();

        // DESCRIPTION
        builder.Property(s => s.Description)
            .HasConversion(
                description => description.Value,
                value => SpecieDescription.Create(value).Value)
            .HasMaxLength(SpecieDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired();

        // CREATED_AT
        builder.Property(s => s.CreatedAt)
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