using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using KindPaws.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        // ID
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(
                petId => petId.Value,
                value => VolunteerId.Create(value).Value)
            .HasColumnName("id");

        // FULLNAME
        builder.ComplexProperty(v => v.FullName, f =>
        {
            f.Property(x => x.FirstName)
                .HasMaxLength(FullNameConstraints.MaxFirstNameLength)
                .HasColumnName("first_name")
                .IsRequired();

            f.Property(x => x.LastName)
                .HasMaxLength(FullNameConstraints.MaxLastNameLength)
                .HasColumnName("last_name")
                .IsRequired();

            f.Property(x => x.Patronymic)
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

        // PHONE NUMBER
        builder.ComplexProperty(volunteer => volunteer.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(x => x.Value)
                .HasMaxLength(PhoneNumberConstraints.MaxLength)
                .HasColumnName("phone_number")
                .IsRequired();
        });

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasConversion(
                d => d!.Value,
                d => MediumDescription.Create(d).Value)
            .HasMaxLength(MediumDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);
        
        // ADDRESS
        builder.Property(p => p.Address)
            .HasColumnName("address")
            .MapJsonb()
            .IsRequired(false); // nullable json
        
        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasConversion(
                y => y!.Value,
                y => YearsOfExperience.Create(y).Value)
            .HasColumnName("years_of_experience")
            .IsRequired(false);
        
        // SOCIAL NETWORKS
        builder.Property(p => p.SocialNetworks)
            .HasColumnName("social_networks")
            .MapJsonb()
            .IsRequired();
        
        // REQUISITES
        builder.Property(p => p.Requisites)
            .HasColumnName("requisites")
            .MapJsonb()
            .IsRequired();

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        // PETS AUTO INCLUDE
        builder.Navigation(v => v.Pets).AutoInclude();

        // SOFT DELETE
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}