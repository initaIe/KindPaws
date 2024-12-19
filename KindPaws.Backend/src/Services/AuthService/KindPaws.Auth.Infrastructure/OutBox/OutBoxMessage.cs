using System.Text.Json;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Infrastructure.OutBox;

public class OutBoxMessage
{
    private OutBoxMessage(
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

    public static OutBoxMessage CreateNew<T>(T message)
        where T : IEvent
    {
        var payload = JsonSerializer.Serialize(message, System.Type.GetType(message.EventType)!);
        return new OutBoxMessage(
            message.EventId,
            message.EventType,
            payload,
            message.OccurredAt,
            null,
            null);
    }

    public static OutBoxMessage Create(
        Guid id,
        string type,
        string payload,
        DateTimeOffset occuredAt,
        DateTimeOffset? processedAt,
        string? error)
    {
        return new OutBoxMessage(
            id,
            type,
            payload,
            occuredAt,
            processedAt,
            error);
    }
}