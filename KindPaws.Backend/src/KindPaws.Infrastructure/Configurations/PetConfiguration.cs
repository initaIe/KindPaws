using System.Text.Json;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using KindPaws.Infrastructure.Extensions;
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
                value => PetId.Create(value).Value)
            .HasColumnName("id");

        // PET TYPE
        builder.ComplexProperty(pet => pet.PetType, petType =>
        {
            petType.Property(x => x.SpecieId)
                .HasConversion(
                    specieId => specieId.Value,
                    value => SpecieId.Create(value).Value)
                .HasColumnName("specie_id")
                .IsRequired();

            petType.Property(x => x.BreedId)
                .HasColumnName("breed_guid")
                .IsRequired();
        });

        // NAME
        builder.ComplexProperty(pet => pet.Name, name =>
        {
            name.Property(pet => pet.Value)
                .HasMaxLength(ShortNameConstraints.MaxLength)
                .HasColumnName("name")
                .IsRequired();
        });

        // CREATION DATE
        builder.Property(pet => pet.CreationDateTime)
            .HasColumnName("creation_date")
            .IsRequired();

        // SUPPORT STATUS
        builder.Property(pet => pet.SupportStatus)
            .HasConversion(
                s => s!.Value,
                s => SupportStatus.Create(s).Value)
            .HasMaxLength(SupportStatusConstraints.MaxLength)
            .HasColumnName("support_status")
            .IsRequired(false);

        // DESCRIPTION
        builder.Property(pet => pet.Description)
            .HasConversion(
                d => d!.Value,
                d => MediumDescription.Create(d).Value)
            .HasMaxLength(MediumDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // PET COLOR
        builder.Property(pet => pet.PetColor)
            .HasConversion(
                c => c!.Value,
                c => PetColor.Create(c).Value)
            .HasMaxLength(PetColorConstraints.MaxLength)
            .HasColumnName("color")
            .IsRequired(false);

        // AGE
        builder.Property(pet => pet.Age)
            .HasConversion(
                age => age!.DateBirth,
                age => Age.Create(age).Value)
            .HasColumnName("age")
            .IsRequired(false);

        // HEALTH DETAILS
        builder.ComplexProperty(p => p.HealthDetails, health =>
        {
            health.ComplexProperty(h => h.Description, description =>
            {
                description.Property(x => x!.Value)
                    .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                    .HasColumnName("description")
                    .IsRequired();
            });

            health.ComplexProperty(h => h.HealthStatus, helpStatus =>
            {
                helpStatus.Property(x => x!.Value)
                    .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                    .HasColumnName("helpStatus")
                    .IsRequired(false);
            });

            health.Property(h => h.IsNeutered)
                .HasColumnName("is_neutered")
                .IsRequired(false);

            health.Property(x => x.Vaccines)
                .HasConversion(
                    x=>JsonSerializer.Serialize(x, JsonSerializerOptions.Default),
                    x=>JsonSerializer.Deserialize<List<Vaccine>>(x, JsonSerializerOptions.Default)!)
                .HasColumnName("vaccines")
                .HasColumnType("jsonb") // Указываем тип как jsonb
                .IsRequired();

            health.Property(x => x.Diseases)
                .HasConversion(
                    x=>JsonSerializer.Serialize(x, JsonSerializerOptions.Default),
                    x=>JsonSerializer.Deserialize<List<Disease>>(x, JsonSerializerOptions.Default)!)
                .HasColumnName("diseases")
                .HasColumnType("jsonb") // Указываем тип как jsonb
                .IsRequired();
        });


        // builder.Property(pet => pet.HealthDetails)
        //     .HasJsonConversion()
        //     .HasColumnName("health_details")
        //     .HasColumnType("jsonb")
        //     .IsRequired();

        // BIOMETRIC DETAILS
        builder.Property(pet => pet.BiometricDetails)
            .HasJsonConversion()
            .HasColumnName("biometric_details")
            .HasColumnType("jsonb")
            .IsRequired();

        // PHOTOS DETAILS
        builder.Property(pet => pet.Photos)
            .HasJsonConversion()
            .HasColumnName("photos")
            .HasColumnType("jsonb")
            .IsRequired();

        // SOFT DELETE
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}