using KindPaws.Core.OutBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Core.OutBox.Database;

public class OutBoxMessagesConfiguration : IEntityTypeConfiguration<OutBoxMessage>
{
    public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        // TABLE NAMING
        builder.ToTable("outbox_messages");

        // ID
        builder.HasKey(outBoxMessage => outBoxMessage.Id);
        builder.Property(outBoxMessage => outBoxMessage.Id)
            .HasColumnName("id");

        // TYPE
        builder.Property(outBoxMessage => outBoxMessage.Type)
            .HasMaxLength(512)
            .HasColumnName("type")
            .IsRequired();

        // PAYLOAD
        builder.Property(outBoxMessage => outBoxMessage.Payload)
            .HasColumnType("jsonb")
            .HasColumnName("payload")
            .IsRequired();

        // OCCURED_AT
        builder.Property(outBoxMessage => outBoxMessage.OccuredAt)
            .HasColumnName("occured_at")
            .IsRequired();

        // PROCESSED_AT
        builder.Property(outBoxMessage => outBoxMessage.ProcessedAt)
            .HasColumnName("processed_at")
            .IsRequired(false);

        // ERROR
        builder.Property(outBoxMessage => outBoxMessage.Error)
            .HasColumnType("text")
            .HasColumnName("error")
            .IsRequired(false);

        // INDEX
        builder.HasIndex(outBoxMessage => new
            {
                outBoxMessage.OccuredAt,
                outBoxMessage.ProcessedAt,
            })
            .HasDatabaseName("idx_outbox_messages_unprocessed")
            .HasFilter("processed_at IS NULL");
    }
}