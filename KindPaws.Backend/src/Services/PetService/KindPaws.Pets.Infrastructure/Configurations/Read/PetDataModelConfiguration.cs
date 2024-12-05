using KindPaws.Pets.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Read;

public class PetDataModelConfiguration : IEntityTypeConfiguration<PetDataModel>
{
    public void Configure(EntityTypeBuilder<PetDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("pets");

        // ID
        builder.Property(s => s.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(p => p.LastModifiedAt)
            .HasColumnName("last_modified_at");
        
        // NAME
        builder.Property(p => p.Name)
            .HasColumnName("name");
        
        // SPECIE_ID
        builder.Property(p => p.SpecieId)
            .HasColumnName("specie_id");
        
        // BREED_ID
        builder.Property(p => p.BreedId)
            .HasColumnName("breed_id");
        
        // SUPPORT_STATUS
        builder.Property(p => p.SupportStatus)
            .HasColumnName("support_status");
        
        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasColumnName("description");
        
        // BIRTHDAY_AT
        builder.Property(p => p.BirthdayAt)
            .HasColumnName("birthday_at");
        
        // HEALTH_DETAILS
        // builder.Property(p => p.HealthDetails)
        //     .HasJsonConversion()
        //     .HasColumnType("jsonb")
        //     .HasColumnName("health_details")
        //     .IsRequired(false);
        
        // BIOMETRIC_DETAILS
        // builder.Property(p => p.BiometricDetails)
        //     .HasJsonConversion()
        //     .HasColumnType("jsonb")
        //     .HasColumnName("biometric_details")
        //     .IsRequired(false);
    }
}