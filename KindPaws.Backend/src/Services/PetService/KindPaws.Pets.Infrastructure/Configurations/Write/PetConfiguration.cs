using KindPaws.Core.Extensions;
using KindPaws.Pets.Domain.VolunteersManagement.Entities;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        // TABLE NAMING
        builder.ToTable("pets");

        // ID
        builder.HasKey(p => p.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(p => p.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(p => p.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);
        
        // NAME
        builder.Property(p => p.Name)
            .HasConversion(
                name => name!.Value,
                value => PetName.Create(value).Value)
            .HasColumnType("citext")
            .HasColumnName("name")
            .IsRequired();
        
        // TYPE
        builder.ComplexProperty(p => p.Type, type =>
        {
            type.Property(t => t.SpecieId)
                .HasConversion(
                    id => id!.Value,
                    value => SpecieId.Create(value).Value)
                .HasColumnName("specie_id")
                .IsRequired();
            
            type.Property(t => t.BreedId)
                .HasColumnName("breed_id")
                .IsRequired();
        });
        
        // SUPPORT_STATUS
        builder.Property(p => p.SupportStatus)
            .HasConversion(
                supportStatus => supportStatus!.Value,
                value => SupportStatus.Create(value).Value)
            .HasMaxLength(SupportStatusConstraints.MaxLength)
            .HasColumnName("support_status")
            .IsRequired();
        
        // DESCRIPTION
        builder.Property(p => p.Description)
            .HasConversion(
                description => description!.Value,
                value => PetDescription.Create(value).Value)
            .HasMaxLength(PetDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);
        
        // BIRTHDAY_AT
        builder.Property(p => p.BirthdayAt)
            .HasConversion(
                birthdayAt => birthdayAt!.Value,
                value => BirthdayAt.Create(value).Value)
            .HasColumnName("birthday_at")
            .IsRequired(false);
        
        // HEALTH_DETAILS
        builder.Property(p => p.HealthDetails)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("health_details")
            .IsRequired(false);
        
        // BIOMETRIC_DETAILS
        builder.Property(p => p.BiometricDetails)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("biometric_details")
            .IsRequired(false);
    }
}