using KindPaws.Accounts.Domain.Entities;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");

        // ID
        builder.HasKey(rs => rs.Id);
        builder.Property(rs => rs.Id)
            .HasConversion(
                id => id.Value,
                value => RefreshSessionId.Create(value).Value)
            .HasColumnName("id");

        // JTI
        builder.Property(rs => rs.Jti)
            .HasConversion(
                jti => jti.Value,
                value => Jti.Create(value).Value)
            .HasColumnName("jti")
            .IsRequired();

        // REFRESH_TOKEN
        builder.Property(rs => rs.RefreshToken)
            .HasConversion(
                refreshToken => refreshToken.Value,
                value => RefreshToken.Create(value).Value)
            .HasColumnName("refresh_token")
            .IsRequired();

        // CREATED_AT
        builder.Property(rs => rs.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // EXPIRES_AT
        builder.Property(rs => rs.ExpiresAt)
            .HasConversion(
                expiresAt => expiresAt.Value,
                value => RefreshSessionExpiresAt.Create(value).Value)
            .HasColumnName("expires_at")
            .IsRequired();
    }
}