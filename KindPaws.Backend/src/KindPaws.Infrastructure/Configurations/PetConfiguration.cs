using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Managements.PetManagement.Constraints;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(pet => pet.Id);

        builder.Property(pet => pet.Id)
            .HasConversion(
                petId => petId.Value,
                value => PetId.Create(value));

        builder.Property(pet => pet.Name)
            .HasMaxLength(PetConstraints.MaxNameLength)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(pet => pet.Description)
            .HasMaxLength(PetConstraints.MaxDescriptionLength)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(pet => pet.SpecieId);

        builder.Property(pet => pet.BreedId);

        builder.OwnsOne(pet => pet.HealthDetails, healthDetails =>
        {
            healthDetails.ToJson("health_details");

            healthDetails.Property(x => x.Description);

            healthDetails.OwnsMany(x => x.Vaccines)
                .Property(x => x.Value);

            healthDetails.OwnsMany(x => x.Diseases)
                .Property(x => x.Value);

            healthDetails.OwnsOne(x => x.HealthStatus)
                .Property(x => x.Value);

            healthDetails.Property(x => x.IsNeutered);
        });

        builder.ComplexProperty(pet => pet.CharacteristicsDetails, characteristicsDetails =>
        {
            characteristicsDetails.Property(x => x.Height)
                .HasColumnName("height")
                .IsRequired();

            characteristicsDetails.Property(x => x.Weight)
                .HasColumnName("weight")
                .IsRequired();

            characteristicsDetails.ComplexProperty(x => x.Gender, gender =>
            {
                gender.Property(x => x.Value)
                    .HasColumnName("gender")
                    .IsRequired();
            });
        });

        builder.ComplexProperty(pet => pet.Address, address =>
        {
            address.Property(x => x.Country)
                .HasMaxLength(AddressConstraints.MaxCountryLength)
                .HasColumnName("country")
                .IsRequired();

            address.Property(x => x.City)
                .HasMaxLength(AddressConstraints.MaxCityLength)
                .HasColumnName("city")
                .IsRequired();

            address.Property(x => x.Street)
                .HasMaxLength(AddressConstraints.MaxStreetLength)
                .HasColumnName("street")
                .IsRequired();
        });

        builder.ComplexProperty(pet => pet.Age, age =>
        {
            age.Property(x => x.DateBirth)
                .HasColumnName("date_birth")
                .IsRequired();
        });

        builder.OwnsOne(pet => pet.SupportDetails, helpDetails =>
        {
            helpDetails.ToJson("help_details");

            helpDetails.OwnsOne(x => x.Status)
                .Property(x => x.Value);

            helpDetails.OwnsMany(x => x.Requisites)
                .Property(x => x.Name);

            helpDetails.OwnsMany(x => x.Requisites)
                .Property(x => x.Description);
        });

        builder.OwnsOne(x => x.PhotosList, photosDetails =>
        {
            photosDetails.ToJson("photos_details");

            photosDetails.OwnsMany(x => x.Photos)
                .Property(x => x.PathToStorage);

            photosDetails.OwnsMany(x => x.Photos)
                .Property(x => x.IsMain);
        });

        builder.Property(pet => pet.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();
    }
}