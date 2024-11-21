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

        // ID
        builder.Property(ar => ar.Id)
            .HasConversion(
                id => id.Value,
                value => AccountRoleId.Create(value).Value)
            .HasColumnName("id");

        // ROLE_ID
        builder.Property(ar => ar.RoleId)
            .HasConversion(
                roleId => roleId.Value,
                value => RoleId.Create(value).Value)
            .HasColumnName("role_id");

        // CREATION_TIMESTAMP
        builder.Property(ar => ar.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}