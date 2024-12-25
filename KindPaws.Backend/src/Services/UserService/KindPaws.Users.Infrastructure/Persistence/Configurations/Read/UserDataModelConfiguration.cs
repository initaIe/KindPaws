using KindPaws.Users.Application.Common.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Read;

public class UserDataModelConfiguration : IEntityTypeConfiguration<UserDataModel>
{
    public void Configure(EntityTypeBuilder<UserDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");

        // ID
        builder.Property(user => user.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(user => user.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(user => user.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // USERNAME
        builder.Property(user => user.Username)
            .HasColumnName("username");

        // EMAIL_ADDRESS
        builder.Property(user => user.EmailAddress)
            .HasColumnName("email_address");

        // PHONE_NUMBER
        builder.Property(user => user.PhoneNumber)
            .HasColumnName("phone_number");

        // PROFILE
        builder.HasOne(user => user.Profile)
            .WithOne()
            .HasForeignKey<ProfileDataModel>(p => p.UserId);

        // REPUTATION
        builder.Property(user => user.Reputation)
            .HasColumnName("reputation");

        // ACCOUNT_ID
        builder.Property(user => user.AccountId)
            .HasColumnName("account_id");

        // ACCOUNT_ID
        builder.Property(user => user.Roles)
            .HasColumnName("roles");
    }
}