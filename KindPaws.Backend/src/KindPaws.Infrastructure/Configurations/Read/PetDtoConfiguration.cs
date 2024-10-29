using KindPaws.Application.DTOs;
using KindPaws.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDTO>
{
    public void Configure(EntityTypeBuilder<PetDTO> builder)
    {
        builder.ToTable("pets");

        // ID
        builder.Property(p => p.Id)
            .HasColumnName("id");

        // PET TYPE
        builder.Property(p => p.SpecieId)
            .HasColumnName("specie_id");
        builder.Property(p => p.BreedId)
            .HasColumnName("breed_guid");

        // NAME
        builder.Property(p => p.Name)
            .HasColumnName("name");

        // CREATION DATE
        builder.Property(p => p.CreationDateTime)
            .HasColumnName("creation_date_time");

        // SUPPORT STATUS
        builder.Property(p => p.SupportStatus)
            .HasColumnName("support_status");

        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasColumnName("description");

        // PET COLOR
        builder.Property(p => p.Color)
            .HasColumnName("color");

        // AGE
        builder.Property(p => p.Age)
            .HasColumnName("date_birth");

        // HEALTH DETAILS
        builder.Property(p => p.HealthDetails)
            .HasColumnName("health_details")
            .MapJsonb();

        // BIOMETRIC DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasColumnName("biometric_details")
            .MapJsonb();

        // PHOTOS DETAILS
        builder.Property(p => p.Photos)
            .HasColumnName("photos")
            .MapJsonb();

        // POSITION
        builder.Property(p => p.Position)
            .HasColumnName("position");
        
        // VOLUNTEER ID
        builder.Property(p => p.VolunteerId)
            .HasColumnName("volunteer_id");
    }
}