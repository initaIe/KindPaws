using System.Text.Json;
using KindPaws.Auth.Application.DataModels;
using KindPaws.Auth.Application.Mappers;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Read;

public class AccountDataModelConfiguration : IEntityTypeConfiguration<AccountDataModel>
{
    public void Configure(EntityTypeBuilder<AccountDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("accounts");

        // ID
        builder.Property(a => a.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(a => a.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // USER_NAME
        builder.Property(a => a.UserName)
            .HasColumnName("user_name");

        // EMAIL_ADDRESS
        builder.Property(a => a.EmailAddress)
            .HasColumnName("email_address");

        // PHONE_NUMBER
        builder.Property(a => a.PhoneNumber)
            .HasColumnName("phone_number");

        // PASSWORD_HASH
        builder.Property(a => a.PasswordHash)
            .HasColumnName("password_hash");

        // ROLES
        builder.Property(a => a.Roles)
            .HasColumnName("roles");

        // REFRESH_SESSIONS
        builder.Property(a => a.RefreshSessions)
            .HasConversion(
                refreshSessions => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<RefreshSession>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection())
            .HasColumnName("refresh_sessions");
    }
}