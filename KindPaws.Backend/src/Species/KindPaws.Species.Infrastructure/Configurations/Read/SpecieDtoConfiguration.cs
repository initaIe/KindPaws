using KindPaws.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Species.Infrastructure.Configurations.Read;

public class SpecieDtoConfiguration : IEntityTypeConfiguration<SpecieDto>
{
    public void Configure(EntityTypeBuilder<SpecieDto> builder)
    {
        builder.ToTable("species");

        // ID
        builder.Property(specie => specie.Id)
            .HasColumnName("id");

        // NAME
        builder.Property(specie => specie.Name)
            .HasColumnName("name")
            .HasColumnType("citext");

        // DESCRIPTION
        builder.Property(specie => specie.Description)
            .HasColumnName("description");

        // BREEDS
        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey(b => b.SpecieId);

        // IS SOFT DELETED
        builder.Property(s => s.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(s => !s.IsSoftDeleted);
    }
}