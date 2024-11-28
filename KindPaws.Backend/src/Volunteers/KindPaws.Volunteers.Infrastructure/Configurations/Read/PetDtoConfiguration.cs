using System.Text.Json;
using KindPaws.Volunteers.Application.DataModels;
using KindPaws.Volunteers.Application.Mappers;
using KindPaws.Volunteers.Contracts.Dtos;
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

        // PET TYPE
        builder.Property(p => p.SpecieId)
            .HasColumnName("specie_id");
        builder.Property(p => p.BreedId)
            .HasColumnName("breed_id");

        // NAME
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("citext");

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

        // Birthday
        builder.Property(p => p.Birthday)
            .HasColumnName("birthday");

        // HEALTH DETAILS
        builder.Property(p => p.HealthDetails)
            .HasColumnName("health_details")
            .HasColumnType("jsonb")
            .HasConversion(
                healthDetails => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<HealthDetails>(json, JsonSerializerOptions.Default)!.ToDto());

        // BIOMETRIC DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasColumnName("biometric_details")
            .HasColumnType("jsonb")
            .HasConversion(
                biometricDetails => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<BiometricDetails>(json, JsonSerializerOptions.Default)!.ToDto());

        // PHOTOS DETAILS
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

        // VOLUNTEER ID
        builder.Property(p => p.VolunteerId)
            .HasColumnName("volunteer_id");

        // IS SOFT DELETED
        builder.Property(p => p.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(p => !p.IsSoftDeleted);
    }
}