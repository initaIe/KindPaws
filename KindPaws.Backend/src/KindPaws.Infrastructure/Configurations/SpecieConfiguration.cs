using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.Constraints.VOsConstraints;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

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
                value => SpecieId.Create(value))
            .HasColumnName("id");

        // BREEDS
        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey("specie_id")
            .OnDelete(DeleteBehavior.Cascade);

        // NAME
        builder.ComplexProperty(specie => specie.Name, name =>
        {
            name.Property(x => x.Value)
                .HasMaxLength(ShortNameConstraints.MaxLength)
                .HasColumnName("name")
                .IsRequired();
        });

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
    }
}