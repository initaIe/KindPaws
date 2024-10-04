using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects.Constraints;
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
                value => VolunteerId.Create(value))
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
            // emailAddress.HasIndex(x => x.Value)
            //     .IsUnique();
            emailAddress.Property(x => x.Value)
                .HasMaxLength(FullNameConstraints.MaxFirstNameLength)
                .HasColumnName("email_address")
                .IsRequired();
        });

        // DESCRIPTION
        builder.ComplexProperty(volunteer => volunteer.Description, description =>
        {
            description.Property(x => x.Value)
                .HasMaxLength(MediumDescriptionConstraints.MaxLength)
                .HasColumnName("description")
                .IsRequired(false);
        });

        // ADDRESS
        builder.ComplexProperty(volunteer => volunteer.Address, address =>
        {
            address.Property(x => x.Country)
                .HasMaxLength(AddressConstraints.MaxCountryLength)
                .HasColumnName("country")
                .IsRequired(false);

            address.Property(x => x.City)
                .HasMaxLength(AddressConstraints.MaxCityLength)
                .HasColumnName("city")
                .IsRequired(false);

            address.Property(x => x.Street)
                .HasMaxLength(AddressConstraints.MaxStreetLength)
                .HasColumnName("street")
                .IsRequired(false);
        });

        // EXPERIENCE
        builder.ComplexProperty(volunteer => volunteer.Experience, experience =>
        {
            experience.Property(x => x.Value)
                .HasColumnName("experience")
                .IsRequired(false);
        });

        // PHONE NUMBER
        builder.ComplexProperty(volunteer => volunteer.PhoneNumber, phoneNumber =>
        {
            // phoneNumber.HasIndex(volunteer => volunteer.Value)
            //     .IsUnique();
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
    }
}