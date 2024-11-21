using KindPaws.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class AccountRoleDtoConfiguration : IEntityTypeConfiguration<AccountRoleDto>
{
    public void Configure(EntityTypeBuilder<AccountRoleDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("account_roles");

        // ID
        builder.Property(ar => ar.Id)
            .HasColumnName("id");

        // ACCOUNT_ID
        builder.Property(ar => ar.AccountId)
            .HasColumnName("account_id");

        // ROLE_ID
        builder.Property(ar => ar.RoleId)
            .HasColumnName("role_id");

        // CREATION_TIMESTAMP
        builder.Property(ar => ar.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}