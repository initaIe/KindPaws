using KindPaws.Auth.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Read;

public class RefreshSessionDataModelConfiguration : IEntityTypeConfiguration<RefreshSessionDataModel>
{
    public void Configure(EntityTypeBuilder<RefreshSessionDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");

        // ID
        builder.Property(refreshSession => refreshSession.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(refreshSession => refreshSession.CreatedAt)
            .HasColumnName("created_at");

        // LAST_MODIFIED_AT
        builder.Property(refreshSession => refreshSession.LastModifiedAt)
            .HasColumnName("last_modified_at");
        
        // JTI
        builder.Property(refreshSession => refreshSession.Jti)
            .HasColumnName("jti");
        
        // REFRESH_TOKEN
        builder.Property(refreshSession => refreshSession.RefreshToken)
            .HasColumnName("refresh_token");
        
        // EXPIRES_AT
        builder.Property(refreshSession => refreshSession.ExpiresAt)
            .HasColumnName("expires_at");
        
        // ACCOUNT_ID
        builder.Property(refreshSession => refreshSession.AccountId)
            .HasColumnName("account_id");
    }
}