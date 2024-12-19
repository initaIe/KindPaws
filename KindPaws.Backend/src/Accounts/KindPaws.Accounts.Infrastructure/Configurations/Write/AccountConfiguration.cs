using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // TABLE NAMING
        builder.ToTable("accounts");

        // ID
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("id");

        // USER_NAME
        builder.Property(a => a.Username)
            .HasConversion(
                userName => userName.Value,
                value => Username.Create(value).Value)
            .HasColumnName("user_name")
            .HasColumnType("citext")
            .IsRequired();

        // EMAIL
        builder.Property(a => a.EmailAddress)
            .HasConversion(
                emailAddress => emailAddress.Value,
                value => EmailAddress.Create(value).Value)
            .HasColumnName("email_address")
            .HasColumnType("citext")
            .IsRequired();

        // PASSWORD_HASH
        builder.Property(a => a.PasswordHash)
            .HasConversion(
                passwordHash => passwordHash.Value,
                value => PasswordHash.Create(value).Value)
            .HasColumnName("password_hash")
            .IsRequired();

        // PHONE_NUMBER
        builder.Property(a => a.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber!.Value,
                value => PhoneNumber.Create(value).Value)
            .HasColumnName("phone_number")
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .IsRequired(false);

        // FULL_NAME
        builder.Property(a => a.FullName)
            .HasJsonConversion()
            .HasColumnName("full_name")
            .HasColumnType("jsonb")
            .IsRequired(false);

        // CREATED_AT
        builder.Property(a => a.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // SOCIAL_NETWORKS
        builder.Property(a => a.SocialNetworks)
            .HasJsonConversion()
            .HasColumnName("social_networks")
            .HasColumnType("jsonb")
            .IsRequired();

        // REFRESH_SESSIONS
        builder.HasMany(a => a.RefreshSessions)
            .WithOne()
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // ACCOUNT_ROLES
        builder.Property(a => a.Roles)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("roles")
            .IsRequired();
    }
}