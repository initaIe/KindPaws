using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
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
        
        // CREATED_AT
        builder.Property(v => v.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

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

        // IS SOFT DELETE
        builder.Property(b => b.IsSoftDeleted)
            .HasColumnName("is_soft_deleted")
            .IsRequired();

        // SOFT_DELETED_AT
        builder.Property(v => v.SoftDeletedAt)
            .HasColumnName("soft_deleted_at")
            .IsRequired(false);
    }
}