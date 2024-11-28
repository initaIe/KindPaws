using KindPaws.Accounts.Application.DataModels;
using KindPaws.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class RefreshSessionDtoConfiguration : IEntityTypeConfiguration<RefreshSessionDataModel>
{
    public void Configure(EntityTypeBuilder<RefreshSessionDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");

        // ID
        builder.Property(rs => rs.Id)
            .HasColumnName("id");

        // JTI
        builder.Property(rs => rs.Jti)
            .HasColumnName("jti");

        // REFRESH_TOKEN
        builder.Property(rs => rs.RefreshToken)
            .HasColumnName("refresh_token");

        // CREATION_TIMESTAMP
        builder.Property(rs => rs.CreationTimestamp)
            .HasColumnName("creation_timestamp");

        // EXPIRE_TIMESTAMP
        builder.Property(rs => rs.ExpireTimestamp)
            .HasColumnName("expire_timestamp");

        // ACCOUNT_ID
        builder.Property(rs => rs.AccountId)
            .HasColumnName("account_id");
    }
}