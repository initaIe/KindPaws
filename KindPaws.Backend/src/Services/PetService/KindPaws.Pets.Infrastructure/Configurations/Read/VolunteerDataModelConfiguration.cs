using System.Text.Json;
using KindPaws.Pets.Application.DataModels;
using KindPaws.Pets.Application.Mappers;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Read;

public class VolunteerDataModelConfiguration : IEntityTypeConfiguration<VolunteerDataModel>
{
    public void Configure(EntityTypeBuilder<VolunteerDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteers");

        // ID
        builder.Property(v => v.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(v => v.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasColumnName("description");

        // YEARS_OF_EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasColumnName("years_of_experience");

        // REQUISITES
        builder.Property(p => p.Requisites)
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<Requisite>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection())
            .HasColumnName("requisites");

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);
    }
}