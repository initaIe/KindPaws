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

namespace KindPaws.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        // ID
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
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
                .HasColumnType("citext")
                .IsRequired();
        });

        // CREATION DATE
        builder.Property(p => p.CreationDateTime)
            .HasColumnName("creation_date_time")
            .IsRequired();

        // SUPPORT STATUS
        builder.Property(p => p.SupportStatus)
            .HasConversion(
                s => s!.Value,
                value => SupportStatus.Create(value).Value)
            .HasMaxLength(SupportStatusConstraints.MaxLength)
            .HasColumnName("support_status")
            .IsRequired(false);

        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasConversion(
                d => d!.Value,
                value => MediumDescription.Create(value).Value)
            .HasMaxLength(MediumDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // PET COLOR
        builder.Property(p => p.Color)
            .HasConversion(
                d => d!.Value,
                value => PetColor.Create(value).Value)
            .HasMaxLength(PetColorConstraints.MaxLength)
            .HasColumnName("color")
            .IsRequired(false);

        // AGE
        builder.Property(p => p.Age)
            .HasConversion(
                a => a!.DateBirth,
                value => Age.Create(value).Value)
            .HasColumnName("date_birth")
            .IsRequired(false);

        // HEALTH DETAILS
        builder.Property(p => p.HealthDetails)
            .HasColumnName("health_details")
            .MapJsonb()
            .IsRequired();

        // BIOMETRIC DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasColumnName("biometric_details")
            .MapJsonb()
            .IsRequired();

        // PHOTOS DETAILS
        builder.Property(p => p.Photos)
            .HasColumnName("photos")
            .MapJsonb()
            .IsRequired();

        // POSITION
        builder.ComplexProperty(p => p.Position, pb =>
        {
            pb.Property(p => p.Value)
                .HasColumnName("position")
                .IsRequired();
        });

        // SOFT DELETE
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}