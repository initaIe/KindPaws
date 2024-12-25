using System.Text.Json;
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.MessageBox.Entities;

public class BoxMessage : IBoxMessage
{
    private protected BoxMessage(
        Guid id,
        string type,
        string payload,
        DateTimeOffset occuredAt,
        DateTimeOffset? processedAt,
        string? error)
    {
        Id = id;
        Type = type;
        Payload = payload;
        OccuredAt = occuredAt;
        ProcessedAt = processedAt;
        Error = error;
    }

    public Guid Id { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Type { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Payload { get; init; }
    public DateTimeOffset OccuredAt { get; init; }
    public DateTimeOffset? ProcessedAt { get; set; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? Error { get; set; }

    public static BoxMessage CreateNew<T>(T message)
        where T : IEvent
    {
        var outBoxMessageId = Guid.NewGuid();
        var outBoxMessageOccuredAt = DateTimeOffset.UtcNow;

        var payload = JsonSerializer.Serialize(message, System.Type.GetType(message.EventType)!);
        return new BoxMessage(
            outBoxMessageId,
            message.EventType,
            payload,
            outBoxMessageOccuredAt,
            null,
            null);
    }

    public static BoxMessage Create(
        Guid id,
        string type,
        string payload,
        DateTimeOffset occuredAt,
        DateTimeOffset? processedAt,
        string? error)
    {
        return new BoxMessage(
            id,
            type,
            payload,
            occuredAt,
            processedAt,
            error);
    }
}