using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
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
            .HasColumnName("jti");

        // REFRESH_TOKEN
        builder.Property(rs => rs.RefreshToken)
            .HasConversion(
                refreshToken => refreshToken.Value,
                value => RefreshToken.Create(value).Value)
            .HasColumnName("refresh_token");

        // CREATION_TIMESTAMP
        builder.Property(rs => rs.CreationTimestamp)
            .HasColumnName("creation_timestamp");

        // EXPIRE_TIMESTAMP
        builder.Property(rs => rs.ExpireTimestamp)
            .HasColumnName("expire_timestamp");
    }
}