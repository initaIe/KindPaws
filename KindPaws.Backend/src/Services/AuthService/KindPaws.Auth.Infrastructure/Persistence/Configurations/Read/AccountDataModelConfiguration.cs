using KindPaws.Auth.Application.Common.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Persistence.Configurations.Read;

public class AccountDataModelConfiguration : IEntityTypeConfiguration<AccountDataModel>
{
    public void Configure(EntityTypeBuilder<AccountDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("accounts");

        // ID
        builder.Property(account => account.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(account => account.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(account => account.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // USER_NAME
        builder.Property(account => account.UserName)
            .HasColumnName("user_name");

        // EMAIL_ADDRESS
        builder.Property(account => account.EmailAddress)
            .HasColumnName("email_address");

        // PHONE_NUMBER
        builder.Property(account => account.PhoneNumber)
            .HasColumnName("phone_number");

        // PASSWORD_HASH
        builder.Property(account => account.PasswordHash)
            .HasColumnName("password_hash");

        // ROLES
        builder.Property(account => account.Roles)
            .HasColumnName("roles");

        // REFRESH_SESSIONS
        builder.HasMany(account => account.RefreshSessions)
            .WithOne()
            .HasForeignKey(refreshSession => refreshSession.AccountId);
    }
}