using System.Text.Json;
using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Volunteers.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        // ID
        builder.Property(v => v.Id)
            .HasColumnName("id");

        // FULLNAME
        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("citext");

            fb.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasColumnType("citext");

            fb.Property(x => x.Patronymic)
                .HasColumnName("patronymic")
                .HasColumnType("citext");
        });

        // EMAIL ADDRESS
        builder.Property(v => v.EmailAddress)
            .HasColumnName("email_address")
            .HasColumnType("citext");

        // PHONE NUMBER
        builder.Property(v => v.PhoneNumber)
            .HasColumnName("phone_number");

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasColumnName("description");

        // ADDRESS
        builder.Property(p => p.Address)
            .HasColumnName("address")
            .HasColumnType("jsonb")
            .HasConversion(
                address => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<Address>(json, JsonSerializerOptions.Default)!.ToDto());

        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasColumnName("years_of_experience");

        // SOCIAL NETWORKS
        builder.Property(p => p.SocialNetworks)
            .HasColumnName("social_networks")
            .HasColumnType("jsonb")
            .HasConversion(
                socialNetworks => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<SocialNetwork>>(json, JsonSerializerOptions.Default)!
                    .Select(s => s.ToDto()).ToArray());

        // REQUISITES
        builder.Property(p => p.Requisites)
            .HasColumnName("requisites")
            .HasColumnType("jsonb")
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<Requisite>>(json, JsonSerializerOptions.Default)!
                    .Select(r => r.ToDto()).ToArray());

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);

        // IS SOFT DELETED
        builder.Property(v => v.IsSoftDeleted)
            .HasColumnName("is_soft_deleted");

        // QUERY FILTER IS SOT DELETED
        builder.HasQueryFilter(v => !v.IsSoftDeleted);
    }
}