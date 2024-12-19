using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");

        // ID
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(u => u.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(u => u.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // USER_NAME
        builder.Property(u => u.Username)
            .HasConversion(
                userName => userName.Value,
                value => Username.Create(value).Value)
            .HasColumnType("citext")
            .HasColumnName("username")
            .IsRequired();

        // EMAIL_ADDRESS
        builder.Property(u => u.EmailAddress)
            .HasConversion(
                emailAddress => emailAddress.Value,
                value => EmailAddress.Create(value).Value)
            .HasMaxLength(EmailAddressConstraints.MaxLength)
            .HasColumnName("email_address")
            .IsRequired();

        // PHONE_NUMBER
        builder.Property(u => u.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber!.Value,
                value => PhoneNumber.Create(value).Value)
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .HasColumnName("phone_number")
            .IsRequired(false);

        // PROFILE
        builder.HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<Profile>("user_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // REPUTATION
        builder.Property(u => u.Reputation)
            .HasConversion(
                reputation => reputation!.Value,
                value => UserReputation.Create(value))
            .HasColumnName("reputation")
            .IsRequired();

        // ACCOUNT_ID
        builder.Property(u => u.AccountId)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("account_id")
            .IsRequired();

        // ROLES
        builder.Property(u => u.Roles)
            .HasJsonConversion()
            .HasColumnName("roles")
            .HasColumnType("jsonb")
            .IsRequired();

        // IGNORE
        builder.Ignore(u => u.DomainEvents);
    }
}