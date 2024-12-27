using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Core.MessageBox.Database;

public class OutBoxMessageConfiguration : BoxMessageConfiguration<OutBoxMessage>
{
    public override void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        base.Configure(builder);
        builder.ToTable("outbox_messages");
        builder.HasIndex(outBoxMessage => new
            {
                outBoxMessage.OccuredAt,
                outBoxMessage.ProcessedAt,
            })
            .HasDatabaseName("idx_outbox_messages_unprocessed")
            .HasFilter("processed_at IS NULL");
    }
}