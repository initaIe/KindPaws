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
            .HasColumnName("name");

        // DESCRIPTION
        builder.Property(b => b.Description)
            .HasColumnName("description");

        // CREATED_AT
        builder.Property(breed => breed.CreatedAt)
            .HasColumnName("created_at");

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // SOFT_DELETED_AT
        builder.Property(breed => breed.SoftDeletedAt)
            .HasColumnName("soft_deleted_at");

        // SPECIE ID
        builder.Property(breed => breed.SpecieId)
            .HasColumnName("specie_id");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(b => !b.IsSoftDeleted);
    }
}