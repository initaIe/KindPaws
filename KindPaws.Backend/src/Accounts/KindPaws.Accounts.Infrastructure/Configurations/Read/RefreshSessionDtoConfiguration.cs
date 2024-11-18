using KindPaws.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Read;

public class RefreshSessionDtoConfiguration : IEntityTypeConfiguration<RefreshSessionDto>
{
    public void Configure(EntityTypeBuilder<RefreshSessionDto> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");
        
        // ID
        builder.Property(rs => rs.Id)
            .HasColumnName("id");
        
        // USER ID
        builder.Property(rs=>rs.UserId)
            .HasColumnName("user_id");
        
        // JTI
        builder.Property(rs => rs.Jti)
            .HasColumnName("jti");
        
        // REFRESH TOKEN
        builder.Property(rs => rs.RefreshToken)
            .HasColumnName("refresh_token");
        
        // EXPIRE TIMESTAMP
        builder.Property(rs => rs.ExpireTimestamp)
            .HasColumnName("expire_timestamp");

        // CREATION TIMESTAMP
        builder.Property(rs => rs.CreationTimestamp)
            .HasColumnName("creation_timestamp");
    }
}