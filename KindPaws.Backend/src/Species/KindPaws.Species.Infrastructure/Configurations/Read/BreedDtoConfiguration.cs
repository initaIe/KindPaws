using KindPaws.Species.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Species.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDataModel>
{
    public void Configure(EntityTypeBuilder<BreedDataModel> builder)
    {
        builder.ToTable("breeds");

        // ID
        builder.Property(breed => breed.Id)
            .HasColumnName("id");

        // NAME
        builder.Property(breed => breed.Name)
            .HasColumnName("name")
            .HasColumnType("citext");

        // DESCRIPTION
        builder.Property(breed => breed.Description)
            .HasColumnName("description");

        // SPECIE ID
        builder.Property(breed => breed.SpecieId)
            .HasColumnName("specie_id");

        // IS SOFT DELETED
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(b => !b.IsSoftDeleted);
    }
}