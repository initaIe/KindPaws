using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Configurations.Write;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        // TABLE NAMING
        builder.ToTable("profiles");

        // ID
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => ProfileId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
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

        // GENDER
        builder.Property(p => p.Gender)
            .HasConversion(
                gender => gender.Value,
                value => Gender.Create(value).Value)
            .HasMaxLength(GenderConstraints.MaxGenderLength)
            .HasColumnName("gender")
            .IsRequired();

        // FULL_NAME
        builder.Property(p => p.FullName)
            .HasJsonConversion()
            .HasColumnName("full_name")
            .HasColumnType("jsonb")
            .IsRequired(false);

        // BIRTHDAY_AT
        builder.Property(p => p.BirthdayAt)
            .HasConversion(
                birthdayAt => birthdayAt!.Value,
                value => BirthdayAt.Create(value).Value)
            .HasColumnName("birthday_at")
            .IsRequired(false);

        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasConversion(
                description => description!.Value,
                value => ProfileDescription.Create(value).Value)
            .HasMaxLength(UserDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // ADDRESS
        builder.Property(p => p.Address)
            .HasJsonConversion()
            .HasColumnName("address")
            .HasColumnType("jsonb")
            .IsRequired(false);

        // SOCIAL_NETWORKS
        builder.Property(p => p.SocialNetworks)
            .HasJsonConversion()
            .HasColumnName("social_networks")
            .HasColumnType("jsonb")
            .IsRequired();
    }
}