using KindPaws.Accounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Accounts.Infrastructure.Configurations.Write;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        builder.ToTable("refresh_sessions");

        builder.HasKey(rs => rs.Id);
        builder.Property(rs => rs.Id);

        builder.HasOne(rs => rs.User)
            .WithMany()
            .HasForeignKey(rs => rs.UserId);

        builder.Property(rs => rs.Jti);
        builder.Property(rs => rs.RefreshToken);
        builder.Property(rs => rs.ExpiresIn);
        builder.Property(rs => rs.CreatedAt);
    }
}