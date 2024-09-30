using KindPaws.Domain.Managements.VolunteerManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteerManagement.Constraints;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.HasKey(volunteer => volunteer.Id);

        builder.Property(volunteer => volunteer.Id)
            .HasConversion(
                petId => petId.Value,
                value => VolunteerId.Create(value));

        builder.HasMany(volunteer => volunteer.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

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

        builder.ComplexProperty(volunteer => volunteer.EmailAddress, emailAddress =>
        {
            emailAddress.Property(x => x.Value)
                .HasMaxLength(FullNameConstraints.MaxFirstNameLength)
                .HasColumnName("email_address")
                .IsRequired();
        });

        builder.Property(volunteer => volunteer.Description)
            .HasMaxLength(VolunteerConstraints.MaxDescriptionLength)
            .IsRequired();

        builder.Property(volunteer => volunteer.Experience)
            .IsRequired();

        builder.ComplexProperty(volunteer => volunteer.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(x => x.Value)
                .HasMaxLength(PhoneNumberConstraints.MaxLength)
                .HasColumnName("phone_number")
                .IsRequired();
        });

        builder.OwnsOne(volunteer => volunteer.SocialNetworks, socialNetworks =>
        {
            socialNetworks.ToJson("social_networks");
            socialNetworks.OwnsMany(x => x.SocialNetworks);
        });
    }
}