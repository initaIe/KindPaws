using System.Text.Json;
using KindPaws.Accounts.Application.DataModels;
using KindPaws.Accounts.Application.Mappers;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class AccountDtoConfiguration : IEntityTypeConfiguration<AccountDataModel>
{
    public void Configure(EntityTypeBuilder<AccountDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("accounts");

        // ID
        builder.Property(a => a.Id)
            .HasColumnName("id");

        // USER_NAME
        builder.Property(a => a.UserName)
            .HasColumnName("user_name");

        // EMAIL
        builder.Property(a => a.EmailAddress)
            .HasColumnName("email_address");

        // PASSWORD_HASH
        builder.Property(a => a.PasswordHash)
            .HasColumnName("password_hash");

        // PHONE_NUMBER
        builder.Property(a => a.PhoneNumber)
            .HasColumnName("phone_number");

        // FULL_NAME
        builder.Property(a => a.FullName)
            .HasConversion(
                fullName => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<FullName>(json, JsonSerializerOptions.Default)!.ToDto())
            .HasColumnName("full_name");

        // CREATION_TIMESTAMP
        builder.Property(a => a.CreationTimestamp)
            .HasColumnName("creation_timestamp")
            .IsRequired();

        // SOCIAL_NETWORKS
        builder.Property(a => a.SocialNetworks)
            .HasConversion(
                socialNetworks => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<SocialNetwork>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection().ToArray())
            .HasColumnName("social_networks");

        // REFRESH_SESSIONS
        builder.HasMany(a => a.RefreshSessions)
            .WithOne()
            .HasForeignKey(rs => rs.AccountId);

        // ROLES
        builder.Property(a => a.Roles)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("roles")
            .IsRequired();
    }
}