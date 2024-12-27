using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindPaws.Core.MessageBox.Database;

public abstract class BoxMessageConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class, IBoxMessage
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
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
    }
}