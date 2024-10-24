using KindPaws.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDTO>
{
    public void Configure(EntityTypeBuilder<PetDTO> builder)
    {
        builder.ToTable("pets");

        // ID
        builder.Property(p => p.Id);

        // PET TYPE
        builder.Property(p => p.SpecieId);
        builder.Property(p => p.BreedId);

        // NAME
        builder.Property(p => p.Name);

        // CREATION DATE
        builder.Property(p => p.CreationDateTime);

        // SUPPORT STATUS
        builder.Property(p => p.SupportStatus);

        // DESCRIPTION
        builder.Property(p => p.Description);

        // PET COLOR
        builder.Property(p => p.Color);

        // AGE
        builder.Property(p => p.Age);

        // HEALTH DETAILS
        builder.Property(p => p.HealthDetails);

        // BIOMETRIC DETAILS
        builder.Property(p => p.BiometricDetails);

        // PHOTOS DETAILS
        builder.Property(p => p.Photos);

        // POSITION
        builder.Property(p => p.Position);
    }
}