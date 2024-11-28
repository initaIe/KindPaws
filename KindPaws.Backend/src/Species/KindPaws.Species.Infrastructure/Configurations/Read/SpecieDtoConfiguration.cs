using KindPaws.Species.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Species.Infrastructure.Configurations.Read;

public class SpecieDtoConfiguration : IEntityTypeConfiguration<SpecieDataModel>
{
    public void Configure(EntityTypeBuilder<SpecieDataModel> builder)
    {
        builder.ToTable("species");

        // ID
        builder.Property(specie => specie.Id)
            .HasColumnName("id");

        // BREEDS
        builder.HasMany(specie => specie.Breeds)
            .WithOne()
            .HasForeignKey(b => b.SpecieId);

        // NAME
        builder.Property(specie => specie.Name)
            .HasColumnName("name");

        // DESCRIPTION
        builder.Property(s => s.Description)
            .HasColumnName("description");

        // CREATED_AT
        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at");

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // SOFT_DELETED_AT
        builder.Property(breed => breed.SoftDeletedAt)
            .HasColumnName("soft_deleted_at");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(s => !s.IsSoftDeleted);
    }
}