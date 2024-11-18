using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Volunteers.Infrastructure.Configurations.Write;

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

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasConversion(
                d => d!.Value,
                d => MediumString.Create(d).Value)
            .HasMaxLength(MediumDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // ADDRESS
        builder.Property(p => p.Address)
            .HasColumnName("address")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired(false); // nullable json

        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasConversion(
                y => y!.Value,
                y => YearsOfExperience.Create(y).Value)
            .HasColumnName("years_of_experience")
            .IsRequired(false);

        // REQUISITES
        builder.Property(p => p.Requisites)
            .HasColumnName("requisites")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired();

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // PETS AUTO INCLUDE
        builder.Navigation(v => v.Pets).AutoInclude();

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT DELETE DATE TIME
        builder.Property(v => v.SoftDeletionTimestamp)
            .HasConversion(
                utc => utc!.Value,
                date => UtcNowTimestamp.Create(date))
            .HasColumnName("soft_delete_datetime")
            .IsRequired(false);
    }
}