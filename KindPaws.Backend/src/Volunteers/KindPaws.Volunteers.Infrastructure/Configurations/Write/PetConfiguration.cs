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
        builder.Property(pet => pet.Name)
            .HasConversion(
                name => name.Value,
                value => PetName.Create(value).Value)
            .HasColumnName("name")
            .HasColumnType("citext")
            .IsRequired();

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // SUPPORT STATUS
        builder.Property(p => p.SupportStatus)
            .HasConversion(
                s => s!.Value,
                value => SupportStatus.Create(value).Value)
            .HasColumnName("support_status")
            .HasColumnType("citext")
            .IsRequired(false);

        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasConversion(
                d => d!.Value,
                value => PetDescription.Create(value).Value)
            .HasColumnName("description")
            .IsRequired(false);

        // PET COLOR
        builder.Property(p => p.Color)
            .HasConversion(
                d => d!.Value,
                value => PetColor.Create(value).Value)
            .HasColumnName("color")
            .HasColumnType("citext")
            .IsRequired(false);

        // BIRTHDAY
        builder.Property(p => p.Birthday)
            .HasConversion(
                birthday => birthday!.Value,
                value => Birthday.Create(value).Value)
            .HasColumnName("birthday")
            .IsRequired(false);

        // HEALTH_DETAILS
        builder.Property(p => p.HealthDetails)
            .HasColumnName("health_details")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired(false);

        // BIOMETRIC DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasColumnName("biometric_details")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired(false);

        // PHOTOS DETAILS
        builder.Property(p => p.Photos)
            .HasColumnName("photos")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired();

        // POSITION
        builder.Property(p => p.Position)
            .HasConversion(
                position => position!.Value,
                value => Position.Create(value).Value)
            .HasColumnName("position")
            .IsRequired();

        // IS_SOFT_DELETED
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT_DELETED_AT
        builder.Property(p => p.SoftDeletedAt)
            .HasColumnName("soft_deleted_at")
            .IsRequired(false);
    }
}