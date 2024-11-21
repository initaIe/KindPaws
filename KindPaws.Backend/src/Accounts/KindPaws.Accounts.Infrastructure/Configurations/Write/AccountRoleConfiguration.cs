using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
{
    public void Configure(EntityTypeBuilder<AccountRole> builder)
    {
        // TABLE NAMING
        builder.ToTable("account_roles");

        // ACCOUNT_ID
        builder.Property(ar => ar.AccountId)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.Create(value).Value)
            .HasColumnName("account_id");

        // ROLE_ID
        builder.Property(ar => ar.RoleId)
            .HasConversion(
                roleId => roleId.Value,
                value => RoleId.Create(value).Value)
            .HasColumnName("role_id");

        // KEY
        builder.HasKey(ar => new { ar.AccountId, ar.RoleId });
    }
}