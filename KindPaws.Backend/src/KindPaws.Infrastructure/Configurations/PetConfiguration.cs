using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        // ID
        builder.HasKey(pet => pet.Id);

        builder.Property(pet => pet.Id)
            .HasConversion(
                petId => petId.Value,
                value => PetId.Create(value))
            .HasColumnName("id");

        // NAME
        builder.ComplexProperty(pet => pet.Name, name =>
        {
            name.Property(pet => pet.Value)
                .HasMaxLength(ShortNameConstraints.MaxLength)
                .HasColumnName("name")
                .IsRequired();
        });

        // DESCRIPTION
        builder.ComplexProperty(pet => pet.Description, description =>
        {
            description.Property(x => x.Value)
                .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                .HasColumnName("description")
                .IsRequired(false);
        });

        // PET TYPE
        builder.ComplexProperty(pet => pet.PetType, petType =>
        {
            petType.Property(x => x.SpecieId)
                .HasConversion(
                    specieId => specieId.Value,
                    value => SpecieId.Create(value))
                .HasColumnName("specie_id");

            // GUID BreedId
            petType.Property(x => x.BreedId)
                .HasColumnName("breed_id");
        });

        // PET COLOR
        builder.ComplexProperty(pet => pet.PetColor, petColor =>
        {
            petColor.Property(x => x.Value)
                .HasMaxLength(PetColorConstraints.MaxLength)
                .HasColumnName("color")
                .IsRequired(false);
        });

        // HEALTH DETAILS
        builder.OwnsOne(pet => pet.HealthDetails, healthDetails =>
        {
            healthDetails.ToJson("health_details");

            healthDetails.OwnsOne(x => x.Description, description =>
            {
                description.Property(x => x.Value)
                    .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                    .HasJsonPropertyName("description")
                    .IsRequired(false);
            });

            healthDetails.OwnsMany(x => x.Vaccines, vaccines =>
            {
                vaccines.Property(x => x.Value)
                    .HasJsonPropertyName("name")
                    .HasMaxLength(VaccineConstraints.MaxLength)
                    .IsRequired();
            });

            healthDetails.OwnsMany(x => x.Diseases, diseases =>
            {
                diseases.Property(x => x.Value)
                    .HasJsonPropertyName("name")
                    .HasMaxLength(DiseaseConstraints.MaxLength)
                    .IsRequired();
            });

            healthDetails.OwnsOne(x => x.HealthStatus, healthStatus =>
            {
                healthStatus.Property(x => x.Value)
                    .HasJsonPropertyName("status")
                    .HasMaxLength(HealthStatusConstraints.MaxLength)
                    .IsRequired(false);
            });

            healthDetails.Property(x => x.IsNeutered)
                .HasJsonPropertyName("is_neutered")
                .IsRequired(false);
        });

        // BIOMETRIC DETAILS
        builder.ComplexProperty(pet => pet.BiometricDetails, biometricDetails =>
        {
            biometricDetails.Property(x => x.Height)
                .HasColumnName("height")
                .IsRequired(false);

            biometricDetails.Property(x => x.Weight)
                .HasColumnName("weight")
                .IsRequired(false);

            biometricDetails.ComplexProperty(pet => pet.Gender, gender =>
            {
                gender.Property(x => x.Value)
                    .HasMaxLength(GenderConstraints.MaxGenderLength)
                    .HasColumnName("gender")
                    .IsRequired(false);
            });
        });

        // AGE
        builder.ComplexProperty(pet => pet.Age, age =>
        {
            age.Property(x => x.DateBirth)
                .HasColumnName("date_birth")
                .IsRequired(false);
        });

        // SUPPORT STATUS
        builder.ComplexProperty(pet => pet.SupportStatus, supportStatus =>
        {
            supportStatus.Property(x => x.Value)
                .HasMaxLength(SupportStatusConstraints.MaxLength)
                .HasColumnName("support_status")
                .IsRequired(false);
        });

        // PHOTOS DETAILS
        builder.OwnsOne(x => x.PetPhotoList, photosDetails =>
        {
            photosDetails.ToJson("photos_details");

            photosDetails.OwnsMany(x => x.Photos, photos =>
                {
                    photos.OwnsOne(x => x.Photo, photo =>
                    {
                        photo.OwnsOne(x => x.PathToStorage, pathToStorage =>
                        {
                            pathToStorage.Property(x => x.Value)
                                .HasJsonPropertyName("path_to_storage")
                                .HasMaxLength(PathToStorageConstraints.MaxLength)
                                .IsRequired();
                        });
                    });
                    photos.Property(x => x.IsMain)
                        .HasJsonPropertyName("is_main")
                        .IsRequired();
                }
            );
        });

        // CREATION DATE
        builder.Property(pet => pet.CreationDateTime)
            .HasColumnName("creation_date")
            .IsRequired();
    }
}