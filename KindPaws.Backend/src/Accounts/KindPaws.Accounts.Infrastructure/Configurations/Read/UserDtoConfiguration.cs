using System.Text.Json;
using KindPaws.Accounts.Application.Helpers;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");
        
        // ID
        builder.Property(u => u.Id)
            .HasColumnName("id");
        
        // USER NAME
        builder.Property(u => u.UserName)
            .HasColumnName("user_name");
        
        // EMAIL ADDRESS
        builder.Property(u => u.Email)
            .HasColumnName("email_address");
        
        // PHONE NUMBER
        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number");
        
        // FULL NAME
        builder.Property(u => u.FullName)
            .HasColumnName("full_name")
            .HasConversion(
                fullName => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<FullName>(json, JsonSerializerOptions.Default)!.ToDto());
        
        // SOCIAL NETWORKS
        builder.Property(u => u.SocialNetworks)
            .HasColumnName("social_networks")
            .HasConversion(
                socialNetworks => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<SocialNetwork>>(json, JsonSerializerOptions.Default)!
                    .Select(sn => sn.ToDto()).ToArray());
    }
}