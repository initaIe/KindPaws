using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Users.Infrastructure.Persistence.Configurations.Write;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        // TABLE NAMING
        builder.ToTable("profiles");

        // ID
        builder.HasKey(profile => profile.Id);
        builder.Property(profile => profile.Id)
            .HasConversion(
                id => id.Value,
                value => ProfileId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(profile => profile.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(profile => profile.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // GENDER
        builder.Property(profile => profile.Gender)
            .HasConversion(
                gender => gender!.Value,
                value => Gender.Create(value).Value)
            .HasMaxLength(GenderConstraints.MaxGenderLength)
            .HasColumnName("gender")
            .IsRequired();

        // FULLNAME
        builder.Property(profile => profile.FullName)
            .HasJsonConversion()
            .HasColumnName("full_name")
            .IsRequired(false);

        // BIRTHDAY_AT
        builder.Property(profile => profile.BirthdayAt)
            .HasConversion(
                birthdayAt => birthdayAt!.Value,
                value => BirthdayAt.Create(value).Value)
            .HasColumnName("birthday_at")
            .IsRequired(false);

        // DESCRIPTION
        builder.Property(profile => profile.Description)
            .HasConversion(
                description => description!.Value,
                value => ProfileDescription.Create(value).Value)
            .HasMaxLength(ProfileDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // ADDRESS
        builder.Property(profile => profile.Address)
            .HasJsonConversion()
            .HasColumnName("address")
            .IsRequired(false);

        // SOCIAL_NETWORKS
        builder.Property(profile => profile.SocialNetworks)
            .HasJsonConversion()
            .HasColumnName("social_networks")
            .IsRequired();
    }
}