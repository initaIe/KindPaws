using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        // ID
        builder.HasKey(u => u.Id);

        // ROLES
        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();
        
        // EMAIL ADDRESS
        builder.Property(u=>u.Email)
            .HasColumnType("citext")
            .HasColumnName("email_address")
            .IsRequired(false);
        
        // USER NAME
        builder.Property(u=>u.UserName)
            .HasColumnType("citext")
            .HasColumnName("user_name")
            .IsRequired();
        
        // FULL NAME
        builder.Property(u => u.FullName)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("full_name")
            .IsRequired(false);
        
        // SOCIAL NETWORKS
        builder.Property(u => u.SocialNetworks)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("social_networks")
            .IsRequired();
        
        // PHONE NUMBER
        builder.Property(u=>u.PhoneNumber)
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .HasColumnName("phone_number")
            .IsRequired(false);
    }
}