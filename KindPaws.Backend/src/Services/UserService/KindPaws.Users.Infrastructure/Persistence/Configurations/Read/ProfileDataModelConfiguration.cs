using System.Text.Json;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Application.Common.DataModels;
using KindPaws.Users.Application.Common.Mappers;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Read;

public class ProfileDataModelConfiguration : IEntityTypeConfiguration<ProfileDataModel>
{
    public void Configure(EntityTypeBuilder<ProfileDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("profiles");

        // ID
        builder.Property(profile => profile.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(profile => profile.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(profile => profile.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // GENDER
        builder.Property(profile => profile.Gender)
            .HasColumnName("gender");

        // FULLNAME
        builder.Property(profile => profile.FullName)
            .HasConversion(
                fullName => string.Empty,
                json => JsonSerializer.Deserialize<FullName>(json, JsonSerializerOptions.Default)!.ToDto())
            .HasColumnName("full_name");

        // BIRTHDAY_AT
        builder.Property(profile => profile.BirthdayAt)
            .HasColumnName("birthday_at");

        // DESCRIPTION
        builder.Property(profile => profile.Description)
            .HasColumnName("description");

        // ADDRESS
        builder.Property(profile => profile.Address)
            .HasConversion(
                address => string.Empty,
                json => JsonSerializer.Deserialize<Address>(json, JsonSerializerOptions.Default)!.ToDto())
            .HasColumnName("address");

        // SOCIAL_NETWORKS
        builder.Property(profile => profile.SocialNetworks)
            .HasConversion(
                socialNetworks => string.Empty,
                json => JsonSerializer.Deserialize<IEnumerable<SocialNetwork>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection())
            .HasColumnName("social_networks");
    }
}