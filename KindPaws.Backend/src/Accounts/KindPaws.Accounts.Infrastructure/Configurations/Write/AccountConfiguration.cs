using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // TABLE NAMING
        builder.ToTable("accounts");
        
        // ID
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id");
        
        // USER NAME
        builder.Property(u => u.UserName)
            .HasColumnType("citext")
            .HasColumnName("user_name")
            .IsRequired();
        builder.HasIndex(u => u.UserName);
        
        // EMAIL ADDRESS
        builder.Property(u => u.Email)
            .HasColumnType("citext")
            .HasColumnName("email_address")
            .IsRequired(false);
        builder.HasIndex(u => u.Email);
        
        // PHONE NUMBER
        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .HasColumnName("phone_number")
            .IsRequired(false);
        builder.HasIndex(u => u.PhoneNumber);
        
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
        
        // RefreshSessions
        builder.Property(u => u.RefreshSessions)
            .HasJsonConversion()
            .HasColumnType("jsonb")
            .HasColumnName("refresh_sessions")
            .IsRequired();

        // ROLES
        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<AccountRole>();
    }
}