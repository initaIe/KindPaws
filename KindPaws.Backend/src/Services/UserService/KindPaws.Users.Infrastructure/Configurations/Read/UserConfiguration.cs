using System.Text.Json;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Users.Application.DataModels;
using KindPaws.Users.Application.Mappers;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Read;

public class UserConfiguration : IEntityTypeConfiguration<UserDataModel>
{
    public void Configure(EntityTypeBuilder<UserDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");

        // ID
        builder.Property(u => u.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(u => u.LastModifiedAt)
            .HasColumnName("last_modified_at");

        // USER_NAME
        builder.Property(u => u.UserName)
            .HasColumnName("username");

        // EMAIL_ADDRESS
        builder.Property(u => u.EmailAddress)
            .HasColumnName("email_address");

        // PROFILE
        builder.HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<ProfileDataModel>(p => p.UserId);

        // ROLES
        builder.Property(u => u.Roles)
            .HasConversion(
                healthDetails => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<RoleId>>(json, JsonSerializerOptions.Default)!
                    .ToDtoCollection())
            .HasColumnName("roles");
    }
}