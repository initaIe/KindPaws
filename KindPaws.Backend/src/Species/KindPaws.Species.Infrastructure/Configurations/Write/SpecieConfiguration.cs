using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;
using KindPaws.Species.Domain.AggregateRoot;
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
                value => ShortAlphabeticWhiteSpacesString.Create(value).Value)
            .HasMaxLength(ShortAlphabeticStringConstraints.MaxLength)
            .HasColumnName("name")
            .HasColumnType("citext")
            .IsRequired();
        builder.HasIndex(s => s.Name).IsUnique();

        // DESCRIPTION
        builder.ComplexProperty(specie => specie.Description, description =>
        {
            description.Property(x => x.Value)
                .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                .HasColumnName("description")
                .IsRequired();
        });

        // Breeds auto include
        builder.Navigation(specie => specie.Breeds).AutoInclude();

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