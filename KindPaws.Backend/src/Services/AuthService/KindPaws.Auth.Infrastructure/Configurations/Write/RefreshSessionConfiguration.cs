using KindPaws.Auth.Domain.AccountsManagement.Entities;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Write;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");

        // ID
        builder.HasKey(refreshSession => refreshSession.Id);
        builder.Property(refreshSession => refreshSession.Id)
            .HasConversion(
                id => id.Value,
                value => RefreshSessionId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(refreshSession => refreshSession.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // LAST_MODIFIED_AT
        builder.Property(refreshSession => refreshSession.LastModifiedAt)
            .HasConversion(
                lastModifiedAt => lastModifiedAt!.Value,
                value => LastModifiedAt.Create(value).Value)
            .HasColumnName("last_modified_at")
            .IsRequired(false);

        // JTI
        builder.Property(refreshSession => refreshSession.Jti)
            .HasConversion(
                jti => jti.Value,
                value => Jti.Create(value).Value)
            .HasColumnName("jti")
            .IsRequired();

        // REFRESH_TOKEN
        builder.Property(refreshSession => refreshSession.RefreshToken)
            .HasConversion(
                refreshToken => refreshToken.Value,
                value => RefreshToken.Create(value).Value)
            .HasColumnName("refresh_token")
            .IsRequired();

        // EXPIRES_AT
        builder.Property(refreshSession => refreshSession.ExpiresAt)
            .HasConversion(
                expiresAt => expiresAt.Value,
                value => RefreshSessionExpiresAt.Create(value).Value)
            .HasColumnName("expires_at")
            .IsRequired();
    }
}