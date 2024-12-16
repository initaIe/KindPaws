using System.Text.Json;
using KindPaws.Volunteers.Application.DataModels;
using KindPaws.Volunteers.Application.Mappers;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Volunteers.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDataModel>
{
    public void Configure(EntityTypeBuilder<PetDataModel> builder)
    {
        builder.ToTable("pets");

        // ID
        builder.Property(p => p.Id)
            .HasColumnName("id");

        // PET_TYPE
        builder.Property(p => p.SpecieId)
            .HasColumnName("specie_id");
        builder.Property(p => p.BreedId)
            .HasColumnName("breed_id");

        // NAME
        builder.Property(pet => pet.Name)
            .HasColumnName("name");

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");

        // SUPPORT_STATUS
        builder.Property(p => p.SupportStatus)
            .HasColumnName("support_status");

        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasColumnName("description");

        // PET_COLOR
        builder.Property(p => p.Color)
            .HasColumnName("color");

        // BIRTHDAY
        builder.Property(p => p.Birthday)
            .HasColumnName("birthday");

        // HEALTH_DETAILS
        builder.Property(p => p.HealthDetails)
            .HasColumnName("health_details")
            .HasColumnType("jsonb")
            .HasConversion(
                healthDetails => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<HealthDetails>(json, JsonSerializerOptions.Default)!.ToDto());

        // BIOMETRIC_DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasColumnName("biometric_details")
            .HasColumnType("jsonb")
            .HasConversion(
                biometricDetails => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<BiometricDetails>(json, JsonSerializerOptions.Default)!.ToDto());

        // PHOTOS_DETAILS
        builder.Property(p => p.Photos)
            .HasColumnName("photos")
            .HasColumnType("jsonb")
            .HasConversion(
                photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<PetPhoto>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection().ToArray());

        // POSITION
        builder.Property(p => p.Position)
            .HasColumnName("position");

        // IS_SOFT_DELETED
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // SOFT_DELETED_AT
        builder.Property(p => p.SoftDeletedAt)
            .HasColumnName("soft_deleted_at");

        // VOLUNTEER_ID
        builder.Property(p => p.VolunteerId)
            .HasColumnName("volunteer_id");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(p => !p.IsSoftDeleted);
    }
}