using KindPaws.Pets.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Read;

public class BreedDataModelConfiguration : IEntityTypeConfiguration<BreedDataModel>
{
    public void Configure(EntityTypeBuilder<BreedDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("breeds");

        // ID
        builder.Property(b => b.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(b => b.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // NAME
        builder.Property(b => b.Name)
            .HasColumnName("name");

        // DESCRIPTION
        builder.Property(b => b.Description)
            .HasColumnName("description");

        // SPECIE_ID
        builder.Property(b => b.SpeciesId)
            .HasColumnName("specie_id");
    }
}