using KindPaws.Accounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        // builder.Property(p => p.SocialNetworks)
        //     .HasColumnName("social_networks")
        //     .HasColumnType("jsonb")
        //     .HasJsonConversion()
        //     .IsRequired();
        //
        // builder.Property(p => p.Requisites)
        //     .HasColumnName("requisites")
        //     .HasColumnType("jsonb")
        //     .HasJsonConversion()
        //     .IsRequired();
    }
}