using KindPaws.Auth.Infrastructure.OutBox;
using KindPaws.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Auth.Infrastructure.Configurations.Write;

public class OutBoxMessagesConfiguration : IEntityTypeConfiguration<OutBoxMessage>
{
    public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        // TABLE NAMING
        builder.ToTable("outbox_messages");
        
        // ID
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .HasColumnName("id");
        
        // TYPE
        builder.Property(d => d.Type)
            .HasMaxLength(256)
            .HasColumnName("type")
            .IsRequired();
        
        // PAYLOAD
        builder.Property(d => d.Payload)
            .HasColumnType("jsonb")
            .HasColumnName("payload")
            .IsRequired();
        
        // OCCURED_AT
        builder.Property(d => d.OccuredAt)
            .HasColumnName("occured_at")
            .IsRequired();
        
        // PROCESSED_AT
        builder.Property(d => d.ProcessedAt)
            .HasColumnName("processed_at")
            .IsRequired(false);
        
        // ERROR
        builder.Property(d => d.Error)
            .HasColumnName("error")
            .IsRequired(false);
        
        // INDEX
        builder.HasIndex(o => new
            {
                o.OccuredAt,
                o.ProcessedAt,
            })
            .HasDatabaseName("idx_outbox_messages_unprocessed")
            .HasFilter("processed_at IS NULL");
    }
}