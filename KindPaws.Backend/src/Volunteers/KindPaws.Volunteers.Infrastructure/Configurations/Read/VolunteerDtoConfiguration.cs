using System.Text.Json;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Application.DataModels;
using KindPaws.Volunteers.Application.Mappers;
using KindPaws.Volunteers.Contracts.Dtos;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Volunteers.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDataModel>
{
    public void Configure(EntityTypeBuilder<VolunteerDataModel> builder)
    {
         // TABLE_NAMING
        builder.ToTable("volunteers");

        // ID
        builder.Property(v => v.Id)
            .HasColumnName("id");

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasColumnName("description");

        // ADDRESS
        builder.Property(p => p.Address)
            .HasColumnName("address")
            .HasConversion(
                address => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<Address>(json, JsonSerializerOptions.Default)!.ToDto());
        
        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasColumnName("years_of_experience");
        
        // CREATED_AT
        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at");

        // REQUISITES
        builder.Property(p => p.Requisites)
            .HasColumnName("requisites")
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<Requisite>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection().ToArray());

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT_DELETED_AT
        builder.Property(v => v.SoftDeletedAt)
            .HasColumnName("soft_deleted_at")
            .IsRequired(false);

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(v => !v.IsSoftDeleted);
    }
}