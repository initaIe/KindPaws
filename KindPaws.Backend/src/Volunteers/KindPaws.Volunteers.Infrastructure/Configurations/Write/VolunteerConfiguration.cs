using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
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
        // TABLE NAMING
        builder.ToTable("volunteers");

        // ID
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value).Value)
            .HasColumnName("id");

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasConversion(
                d => d!.Value,
                d => VolunteerDescription.Create(d).Value)
            .HasMaxLength(VolunteerDescriptionConstraints.MaxLength)
            .HasColumnName("description")
            .IsRequired(false);

        // ADDRESS
        builder.Property(p => p.Address)
            .HasColumnName("address")
            .HasColumnType("jsonb")
            .HasJsonConversion()
            .IsRequired(false);

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
            .HasColumnName("soft_deletion_timestamp")
            .IsRequired(false);
    }
}