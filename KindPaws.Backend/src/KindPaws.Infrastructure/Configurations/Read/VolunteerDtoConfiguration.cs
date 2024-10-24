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
        builder.Property(v => v.Id);

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
        builder.Property(v => v.EmailAddress);

        // PHONE NUMBER
        builder.Property(v => v.PhoneNumber);

        // DESCRIPTION
        builder.Property(v => v.Description);

        // ADDRESS
        builder.Property(p => p.Address)
            .MapJsonb();

        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience);

        // SOCIAL NETWORKS
        builder.Property(p => p.SocialNetworks)
            .MapJsonb();

        // REQUISITES
        builder.Property(p => p.Requisites)
            .MapJsonb();

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}