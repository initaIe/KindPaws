using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
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
        builder.Property(a => a.UserName)
            .HasConversion(
                userName => userName.Value,
                value => UserName.Create(value).Value)
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

        // CREATION_TIMESTAMP
        builder.Property(a => a.CreationTimestamp)
            .HasConversion(
                creationTimestamp => creationTimestamp.Value,
                value => CreationTimestamp.Create(value).Value)
            .HasColumnName("creation_timestamp")
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