using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Volunteers.Domain.Entities;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Volunteers.Infrastructure.Configurations.Write;

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
                .HasColumnName("breed_id")
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
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired();

        // BIOMETRIC DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasColumnName("biometric_details")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired();

        // PHOTOS DETAILS
        builder.Property(p => p.Photos)
            .HasColumnName("photos")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired();

        // POSITION
        builder.ComplexProperty(p => p.Position, pb =>
        {
            pb.Property(p => p.Value)
                .HasColumnName("position")
                .IsRequired();
        });

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT DELETE DATE TIME
        builder.Property(b => b.SoftDeletedDateTime)
            .HasColumnName("soft_delete_datetime")
            .IsRequired(false);

        // HARD DELETE PROPERTY IGNORE
        builder.Ignore(b => b.IsHardDeleted);
    }
}