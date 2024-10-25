using KindPaws.Application.DTOs;
using KindPaws.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDTO>
{
    public void Configure(EntityTypeBuilder<VolunteerDTO> builder)
    {
        builder.ToTable("volunteers");

        // ID
        builder.Property(v => v.Id)
            .HasColumnName("id");

        // FULLNAME
        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(x => x.FirstName)
                .HasColumnName("first_name");

            fb.Property(x => x.LastName)
                .HasColumnName("last_name");

            fb.Property(x => x.Patronymic)
                .HasColumnName("patronymic");
        });

        // EMAIL ADDRESS
        builder.Property(v => v.EmailAddress)
            .HasColumnName("email_address");

        // PHONE NUMBER
        builder.Property(v => v.PhoneNumber)
            .HasColumnName("phone_number");

        // DESCRIPTION
        builder.Property(v => v.Description)
            .HasColumnName("description");

        // ADDRESS
        builder.Property(p => p.Address)
            .HasColumnName("address")
            .MapJsonb();

        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience)
            .HasColumnName("years_of_experience");

        // SOCIAL NETWORKS
        builder.Property(p => p.SocialNetworks)
            .HasColumnName("social_networks")
            .MapJsonb();

        // REQUISITES
        builder.Property(p => p.Requisites)
            .HasColumnName("requisites")
            .MapJsonb();

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}