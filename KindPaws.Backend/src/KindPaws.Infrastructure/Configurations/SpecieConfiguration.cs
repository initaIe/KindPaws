using KindPaws.Domain.Managements.SpecieManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpecieManagement.Constraints;
using KindPaws.Domain.Shared.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class SpecieConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(specie => specie.Id);

        builder.Property(specie => specie.Id)
            .HasConversion(
                specieId => specieId.Value,
                value => SpecieId.Create(value))
            .HasColumnName("id");

        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey("specie_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(specie => specie.Name)
            .HasMaxLength(SpecieConstraints.MaxNameLength)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(specie => specie.Description)
            .HasMaxLength(SpecieConstraints.MaxDescriptionLength)
            .HasColumnName("description")
            .IsRequired();
    }
}