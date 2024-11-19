using KindPaws.Accounts.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
{
    public void Configure(EntityTypeBuilder<AccountRole> builder)
    {
        // TABLE NAMING
        builder.ToTable("account_roles");
        
        // KEY
        builder.HasKey(ar=> new { ar.UserId, ar.RoleId });
        
        // CREATION TIMESTAMP
        builder.Property(ar => ar.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}