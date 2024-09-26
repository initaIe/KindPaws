using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Shared.Constraints;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.VOs;
using KindPaws.Domain.Shared.VOs.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));
        
        builder.ComplexProperty(pet => pet.Name, nameBuilder =>
        {
            nameBuilder.Property(x => x.Value)
                .HasColumnName("name")
                .HasMaxLength(NameConstraints.MaxLength)
                .IsRequired();
        });

        builder.ComplexProperty(pet => pet.Description, descriptionBuilder =>
        {
            descriptionBuilder.Property(x => x.Value)
                .HasColumnName("description")
                .HasMaxLength(DescriptionConstraints.MaxLength)
                .IsRequired();
        });
        
        builder.ComplexProperty(pet => pet.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.City)
                .HasColumnName("city")
                .HasMaxLength(DescriptionConstraints.MaxLength)
                .IsRequired();
            
            addressBuilder.Property(x => x.Country)
                .HasColumnName("country")
                .HasMaxLength(DescriptionConstraints.MaxLength)
                .IsRequired();
            
            addressBuilder.Property(x => x.Street)
                .HasColumnName("street")
                .HasMaxLength(DescriptionConstraints.MaxLength)
                .IsRequired();
        });
        
        builder.ComplexProperty(pet => pet.OwnerPhoneNumber, ownerPhoneNumberBuilder =>
        {
            ownerPhoneNumberBuilder.Property(x => x.Value)
                .HasColumnName("owner_phone_number")
                .HasMaxLength(PhoneNumberConstraints.MaxLength)
                .IsRequired();
        });
     
        builder.ComplexProperty(pet => pet.Age, ageBuilder =>
        {
            ageBuilder.Property(x => x.BirthDate)
                .HasColumnName("birth_date")
                .IsRequired();
        });
    }
}