using System.Text.Json;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

// using JsonSerializerOptions = KindPaws.Infrastructure.Helpers.JsonSerializerOptions;

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
                value => PetId.Create(value).Value)
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
        builder.OwnsOne(pet => pet.Description, description =>
        {
            description.ToJson("description");

            description.Property(x => x.Value)
                .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                .IsRequired(false);
        });

        // PET TYPE
        builder.ComplexProperty(pet => pet.PetType, petType =>
        {
            petType.Property(x => x.SpecieId)
                .HasConversion(
                    specieId => specieId.Value,
                    value => SpecieId.Create(value).Value)
                .HasColumnName("specie_id")
                .IsRequired();

            // GUID BreedId
            petType.Property(x => x.BreedId)
                .HasColumnName("breed_id")
                .IsRequired();
        });

        // PET COLOR
        builder.OwnsOne(pet => pet.PetColor, petColor =>
        {
            petColor.ToJson("color");

            petColor.Property(x => x.Value)
                .HasMaxLength(PetColorConstraints.MaxLength)
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
        builder.Property(pet => pet.BiometricDetails)
            .HasConversion(
                details => JsonSerializer.Serialize(details, JsonSerializerOptions.Default),
                details => JsonSerializer.Deserialize<BiometricDetails>(details, JsonSerializerOptions.Default)!)
            .HasColumnName("biometric_details")
            .HasColumnType("jsonb")
            .IsRequired();

        // builder.OwnsOne(pet => pet.BiometricDetails, biometricDetails =>
        // {
        //     biometricDetails.ToJson("biometric_details");
        //
        //     biometricDetails.OwnsOne(pet => pet.Height, height =>
        //     {
        //         height.Property(x => x.Value)
        //             .HasJsonPropertyName("height")
        //             .IsRequired(false);
        //     });
        //
        //     biometricDetails.OwnsOne(pet => pet.Weight, weight =>
        //     {
        //         weight.Property(x => x.Value)
        //             .HasJsonPropertyName("weight")
        //             .IsRequired(false);
        //     });
        //
        //     biometricDetails.OwnsOne(pet => pet.Gender, gender =>
        //     {
        //         gender.Property(x => x.Value)
        //             .HasMaxLength(GenderConstraints.MaxGenderLength)
        //             .HasJsonPropertyName("gender")
        //             .IsRequired(false);
        //     });
        // });

        // AGE
        builder.Property(pet => pet.Age)
            .HasConversion(
                age => age!.DateBirth,
                age => Age.Create(age).Value)
            .HasColumnName("age")
            .IsRequired(false);

        // builder.OwnsOne(pet => pet.Age, age =>
        // {
        //     age.ToJson("age");
        //
        //     age.Property(x => x!.DateBirth)
        //         .HasJsonPropertyName("date_birth")
        //         .IsRequired(false);
        // });

        // SUPPORT STATUS
        builder.ComplexProperty(pet => pet.SupportStatus, supportStatus =>
        {
            supportStatus.Property(x => x.Value)
                .HasMaxLength(SupportStatusConstraints.MaxLength)
                .HasColumnName("support_status")
                .IsRequired();
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

        // SOFT DELETE
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}