using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Write;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // TABLE NAMING
        builder.ToTable("accounts");

        // ID
        builder.HasKey(account => account.Id);
        builder.Property(account => account.Id)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(account => account.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(account => account.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // USER_NAME
        builder.Property(account => account.UserName)
            .HasConversion(
                userName => userName!.Value,
                value => UserName.Create(value).Value)
            .HasMaxLength(UserNameConstraints.MaxLength)
            .HasColumnName("user_name")
            .IsRequired();
        builder.HasIndex(a => a.UserName).IsUnique();

        // EMAIL_ADDRESS
        builder.Property(account => account.EmailAddress)
            .HasConversion(
                emailAddress => emailAddress!.Value,
                value => EmailAddress.Create(value).Value)
            .HasMaxLength(EmailAddressConstraints.MaxLength)
            .HasColumnName("email_address")
            .IsRequired();
        builder.HasIndex(account => account.EmailAddress).IsUnique();

        // PHONE_NUMBER
        builder.Property(account => account.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber!.Value,
                value => PhoneNumber.Create(value).Value)
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .HasColumnName("phone_number")
            .IsRequired(false);
        builder.HasIndex(account => account.PhoneNumber).IsUnique();

        // PASSWORD_HASH
        builder.Property(account => account.PasswordHash)
            .HasConversion(
                passwordHash => passwordHash!.Value,
                value => PasswordHash.Create(value).Value)
            .HasColumnType("text")
            .HasColumnName("password_hash")
            .IsRequired();

        // ROLES
        builder.Property(account => account.Roles)
            .HasUuidArrayConversion(
                id => id.Value,
                guid => AccountRoleId.Create(guid).Value)
            .HasColumnName("roles");

        // REFRESH_SESSIONS
        builder.HasMany(account => account.RefreshSessions)
            .WithOne()
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // IGNORE
        builder.Ignore(account => account.DomainEvents);
    }
}