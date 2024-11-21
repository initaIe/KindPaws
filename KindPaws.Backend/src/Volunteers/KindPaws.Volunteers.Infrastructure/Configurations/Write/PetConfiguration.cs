using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
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
                .HasColumnName("name")
                .HasColumnType("citext")
                .IsRequired();
        });

        // CREATION DATE
        builder.Property(p => p.CreationTimestamp)
            .HasColumnName("creation_timestamp")
            .IsRequired();

        // SUPPORT STATUS
        builder.Property(p => p.SupportStatus)
            .HasConversion(
                s => s!.Value,
                value => SupportStatus.Create(value).Value)
            .HasMaxLength(SupportStatusConstraints.MaxLength)
            .HasColumnName("support_status")
            .HasColumnType("citext")
            .IsRequired(false);

        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasConversion(
                d => d!.Value,
                value => PetDescription.Create(value).Value)
            .HasMaxLength(PetDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // PET COLOR
        builder.Property(p => p.Color)
            .HasConversion(
                d => d!.Value,
                value => PetColor.Create(value).Value)
            .HasMaxLength(PetColorConstraints.MaxLength)
            .HasColumnName("color")
            .HasColumnType("citext")
            .IsRequired(false);

        // Birthday
        builder.Property(p => p.Birthday)
            .HasConversion(
                a => a!.Value,
                value => Birthday.Create(value).Value)
            .HasColumnName("birthday")
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
        builder.Property(p => p.SoftDeletionTimestamp)
            .HasColumnName("soft_deletion_timestamp")
            .IsRequired(false);
    }
}