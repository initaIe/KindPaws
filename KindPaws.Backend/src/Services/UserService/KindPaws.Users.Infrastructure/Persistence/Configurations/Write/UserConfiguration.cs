using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");

        // ID
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(user => user.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(user => user.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // USERNAME
        builder.Property(user => user.Username)
            .HasConversion(
                username => username!.Value,
                value => Username.Create(value).Value)
            .HasMaxLength(UsernameConstraints.MaxLength)
            .HasColumnName("username")
            .IsRequired();

        // EMAIL_ADDRESS
        builder.Property(user => user.EmailAddress)
            .HasConversion(
                emailAddress => emailAddress!.Value,
                value => EmailAddress.Create(value).Value)
            .HasMaxLength(EmailAddressConstraints.MaxLength)
            .HasColumnName("email_address")
            .IsRequired();

        // PHONE_NUMBER
        builder.Property(user => user.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber!.Value,
                value => PhoneNumber.Create(value).Value)
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .HasColumnName("phone_number")
            .IsRequired(false);

        // PROFILE
        builder.HasOne(user => user.Profile)
            .WithOne()
            .HasForeignKey<Profile>("user_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // REPUTATION
        builder.Property(user => user.Reputation)
            .HasConversion(
                reputation => reputation!.Value,
                value => UserReputation.Create(value))
            .HasColumnName("reputation")
            .IsRequired();

        // ACCOUNT_ID
        builder.Property(user => user.AccountId)
            .HasConversion(
                accountId => accountId!.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("account_id")
            .IsRequired();

        // ACCOUNT_ID
        builder.Property(user => user.Roles)
            .HasUuidArrayConversion(
                userRoleId => userRoleId!.Value,
                value => UserRoleId.Create(value).Value)
            .HasColumnName("roles");
    }
}