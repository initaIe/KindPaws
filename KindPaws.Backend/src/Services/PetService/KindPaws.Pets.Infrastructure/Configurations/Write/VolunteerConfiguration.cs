using KindPaws.Core.Extensions;
using KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Pets.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        // TABLE NAMING
        builder.ToTable("volunteers");

        // ID
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(v => v.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(v => v.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);
        
        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasConversion(
                description => description!.Value,
                value => VolunteerDescription.Create(value).Value)
            .HasMaxLength(VolunteerDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);
        
        // YEARS_OF_EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasConversion(
                yearsOfExperience => yearsOfExperience!.Value,
                value => YearsOfExperience.Create(value).Value)
            .HasColumnName("years_of_experience")
            .IsRequired(false);
        
        // REQUISITES
        builder.Property(v => v.Requisites)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("requisites")
            .IsRequired();
        
        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        // IGNORE
        builder.Ignore(v => v.DomainEvents);
    }
}