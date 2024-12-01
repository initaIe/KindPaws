using KindPaws.Accounts.Application.DataModels;
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

        // CREATED_AT
        builder.Property(rs => rs.CreatedAt)
            .HasColumnName("created_at");

        // EXPIRES_AT
        builder.Property(rs => rs.ExpiresAt)
            .HasColumnName("expires_at");

        // ACCOUNT_ID
        builder.Property(rs => rs.AccountId)
            .HasColumnName("account_id");
    }
}