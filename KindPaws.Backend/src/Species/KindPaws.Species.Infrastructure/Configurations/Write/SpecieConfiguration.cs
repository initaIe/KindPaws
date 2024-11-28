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
        builder.ComplexProperty(specie => specie.Description, description =>
        {
            description.Property(x => x.Value)
                .HasMaxLength(SpecieDescriptionConstraints.MaxLength)
                .HasColumnName("description")
                .IsRequired();
        });

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT DELETE DATE TIME
        builder.Property(breed => breed.SoftDeletionTimestamp)
            .HasColumnName("soft_deletion_timestamp")
            .IsRequired(false);
    }
}