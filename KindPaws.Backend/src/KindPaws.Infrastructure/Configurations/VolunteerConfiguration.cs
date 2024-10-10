using System.Text.Json;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using KindPaws.Infrastructure.Helpers;
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
        builder.Property(volunteer => volunteer.Description)
            .HasConversion(
                v => v!.Value,
                v => MediumDescription.Create(v).Value)
            .HasMaxLength(MediumDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);
        
        // builder.OwnsOne(volunteer => volunteer.Description, description =>
        // {
        //     description.Property(a => a.Value)
        //         .HasMaxLength(AddressConstraints.MaxCityLength)
        //         .HasColumnName("description")
        //         .IsRequired(false);
        //     
        //     description.WithOwner(); 
        // });

        // TODO: rework to JSONB??
        // ADDRESS
        builder.OwnsOne(v => v.Address, address =>
        {
            address.Property(a => a.City)
                .HasMaxLength(AddressConstraints.MaxCityLength)
                .HasColumnName("city")
                .IsRequired(false);

            address.Property(a => a.Street)
                .HasMaxLength(AddressConstraints.MaxStreetLength)
                .HasColumnName("street")
                .IsRequired(false);

            address.WithOwner(); 
        });

        // YEARS OF EXPERIENCE
        builder.Property(x=>x.YearsOfExperience)
            .HasConversion(
                x => x!.Value,
                value => YearsOfExperience.Create(value).Value)
            .HasColumnName("years_of_experience")
            .IsRequired(false);
        
        // (OLD)
        // builder.OwnsOne(volunteer => volunteer.YearsOfExperience, experience =>
        // {
        //     experience.Property(x => x.Value)
        //         .HasColumnName("years_of_experience")
        //         .IsRequired(false);
        //     
        //     experience.WithOwner(); 
        // });
        
        // PHONE NUMBER
        builder.ComplexProperty(volunteer => volunteer.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(x => x.Value)
                .HasMaxLength(PhoneNumberConstraints.MaxLength)
                .HasColumnName("phone_number")
                .IsRequired();
        });

        // SOCIAL NETWORKS
        builder.Property(e => e.SocialNetworkList)
            .HasConversion(ValueConverters.SocialNetworkListConverter)
            .HasColumnName("social_networks")
            .HasColumnType("jsonb");

        // REQUISITES
        builder.Property(e => e.RequisiteList)
            .HasConversion(ValueConverters.RequisiteListConverter)
            .HasColumnName("requisites")
            .HasColumnType("jsonb");

        // PETS
        builder.HasMany(volunteer => volunteer.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        // PETS AUTO INCLUDE
        builder.Navigation(volunteer => volunteer.Pets).AutoInclude();

        // SOFT DELETE
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}