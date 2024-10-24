using KindPaws.Application.DTOs;
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
            fb.Property(x => x.FirstName);

            fb.Property(x => x.LastName);

            fb.Property(x => x.Patronymic);
        });

        // EMAIL ADDRESS
        builder.Property(v => v.EmailAddress);


        // PHONE NUMBER
        builder.Property(v => v.PhoneNumber);

        // DESCRIPTION
        builder.Property(v => v.Description);

        // ADDRESS
        builder.ComplexProperty(v => v.Address, ab =>
        {
            ab.Property(a => a!.City);
            ab.Property(a => a!.Street);
        });

        // YEARS OF EXPERIENCE
        builder.Property(v => v.YearsOfExperience);

        // SOCIAL NETWORKS
        builder.Property(p => p.SocialNetworks);

        // REQUISITES
        builder.Property(p => p.Requisites);

        // PETS
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}