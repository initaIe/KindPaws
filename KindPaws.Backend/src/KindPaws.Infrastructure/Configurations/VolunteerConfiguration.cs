using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        // ID
        builder.HasKey(volunteer => volunteer.Id);

        builder.Property(volunteer => volunteer.Id)
            .HasConversion(
                petId => petId.Value,
                value => VolunteerId.Create(value).Value)
            .HasColumnName("id");

        // FULLNAME
        builder.ComplexProperty(volunteer => volunteer.FullName, fullName =>
        {
            fullName.Property(x => x.FirstName)
                .HasMaxLength(FullNameConstraints.MaxFirstNameLength)
                .HasColumnName("first_name")
                .IsRequired();

            fullName.Property(x => x.LastName)
                .HasMaxLength(FullNameConstraints.MaxLastNameLength)
                .HasColumnName("last_name")
                .IsRequired();

            fullName.Property(x => x.Patronymic)
                .HasMaxLength(FullNameConstraints.MaxPatronymicLength)
                .HasColumnName("patronymic")
                .IsRequired(false);
        });

        // EMAIL ADDRESS
        builder.ComplexProperty(volunteer => volunteer.EmailAddress, emailAddress =>
        {
            emailAddress.Property(x => x.Value)
                .HasMaxLength(FullNameConstraints.MaxFirstNameLength)
                .HasColumnName("email_address")
                .IsRequired();
        });

        // DESCRIPTION
        builder.OwnsOne(volunteer => volunteer.Description, description =>
        {
            description.ToJson("description");

            description.Property(x => x.Value)
                .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                .IsRequired(false);
        });

        // ADDRESS
        builder.OwnsOne(volunteer => volunteer.Address, address =>
        {
            address.ToJson("address");

            address.Property(x => x.City)
                .HasMaxLength(AddressConstraints.MaxCityLength)
                .HasJsonPropertyName("city")
                .IsRequired();

            address.Property(x => x.Street)
                .HasMaxLength(AddressConstraints.MaxStreetLength)
                .HasJsonPropertyName("street")
                .IsRequired();
        });

        // EXPERIENCE
        builder.OwnsOne(volunteer => volunteer.YearsOfExperience, experience =>
        {
            experience.ToJson("experience");

            experience.Property(x => x.Value)
                .IsRequired(false);
        });

        // PHONE NUMBER
        builder.ComplexProperty(volunteer => volunteer.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(x => x.Value)
                .HasMaxLength(PhoneNumberConstraints.MaxLength)
                .HasColumnName("phone_number")
                .IsRequired();
        });

        // SOCIAL NETWORKS
        builder.OwnsOne(volunteer => volunteer.SocialNetworkList, socialNetworks =>
        {
            socialNetworks.ToJson("social_networks");

            socialNetworks.OwnsMany(x => x.SocialNetworks, snb =>
            {
                snb.Property(x => x.Name)
                    .HasJsonPropertyName("name")
                    .HasMaxLength(SocialNetworkConstraints.MaxNameLength)
                    .IsRequired();

                snb.Property(x => x.Link)
                    .HasJsonPropertyName("link")
                    .HasMaxLength(SocialNetworkConstraints.MaxLinkLength)
                    .IsRequired();
            });
        });

        // REQUISITES
        builder.OwnsOne(volunteer => volunteer.RequisiteList, requisites =>
        {
            requisites.ToJson("requisites");

            requisites.OwnsMany(x => x.Requisites, rb =>
            {
                rb.Property(x => x.Name)
                    .HasJsonPropertyName("name")
                    .HasMaxLength(RequisiteConstraints.MaxNameLength)
                    .IsRequired();

                rb.Property(x => x.Description)
                    .HasJsonPropertyName("description")
                    .HasMaxLength(RequisiteConstraints.MinDescriptionLength)
                    .IsRequired();
            });
        });

        // PETS
        builder.HasMany(volunteer => volunteer.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        // PETS AUTO INCLUDE
        builder.Navigation(volunteer => volunteer.Pets).AutoInclude();

        // SOFT DELETE
        builder.Property<bool>("_idDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}