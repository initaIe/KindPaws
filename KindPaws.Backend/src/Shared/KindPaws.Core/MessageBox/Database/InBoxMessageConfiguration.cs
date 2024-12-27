using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Core.MessageBox.Database;

public class InBoxMessageConfiguration : BoxMessageConfiguration<InBoxMessage>
{
    public override void Configure(EntityTypeBuilder<InBoxMessage> builder)
    {
        base.Configure(builder);
        builder.ToTable("inbox_messages");
        builder.HasIndex(outBoxMessage => new
            {
                outBoxMessage.OccuredAt,
                outBoxMessage.ProcessedAt,
            })
            .HasDatabaseName("idx_inbox_messages_unprocessed")
            .HasFilter("processed_at IS NULL");
    }
}