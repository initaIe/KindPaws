using KindPaws.Pets.Application.Common.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Persistence.Configurations.Read;

public class SpecieDataModelConfiguration : IEntityTypeConfiguration<SpecieDataModel>
{
    public void Configure(EntityTypeBuilder<SpecieDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("species");

        // ID
        builder.Property(s => s.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(s => s.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // NAME
        builder.Property(s => s.Name)
            .HasColumnName("name");

        // DESCRIPTION
        builder.Property(s => s.Description)
            .HasColumnName("description");

        // BREEDS
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey(b => b.SpeciesId);
    }
}