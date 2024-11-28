using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
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

        // CREATION_TIMESTAMP
        builder.Property(rs => rs.CreationTimestamp)
            .HasConversion(
                creationTimestamp => creationTimestamp.Value,
                value => CreationTimestamp.Create(value).Value)
            .HasColumnName("creation_timestamp")
            .IsRequired();

        // EXPIRE_TIMESTAMP
        builder.Property(rs => rs.ExpireTimestamp)
            .HasConversion(
                expireTimestamp => expireTimestamp.Value,
                value => RefreshSessionExpireTimestamp.Create(value).Value)
            .HasColumnName("expire_timestamp")
            .IsRequired();
    }
}