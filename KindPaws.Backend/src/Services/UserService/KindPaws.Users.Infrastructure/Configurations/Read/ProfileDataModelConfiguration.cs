using System.Text.Json;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Application.DataModels;
using KindPaws.Users.Application.Mappers;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Read;

public class ProfileDataModelConfiguration : IEntityTypeConfiguration<ProfileDataModel>
{
    public void Configure(EntityTypeBuilder<ProfileDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("profiles");

        // ID
        builder.Property(p => p.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(u => u.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // GENDER
        builder.Property(p => p.Gender)
            .HasColumnName("gender");

        // FULL_NAME
        builder.Property(a => a.FullName)
            .HasConversion(
                fullName => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<FullName>(json, JsonSerializerOptions.Default)!.ToDto())
            .HasColumnName("full_name");

        // BirthdayAt
        builder.Property(p => p.BirthdayAt)
            .HasColumnName("birthday_at");

        // BirthdayAt
        builder.Property(p => p.Description)
            .HasColumnName("description");

        // ADDRESS
        builder.Property(p => p.Address)
            .HasConversion(
                fullName => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<Address>(json, JsonSerializerOptions.Default)!.ToDto())
            .HasColumnName("address");

        // SOCIAL_NETWORKS
        builder.Property(p => p.SocialNetworks)
            .HasConversion(
                fullName => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<SocialNetwork>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection())
            .HasColumnName("social_networks");
    }
}