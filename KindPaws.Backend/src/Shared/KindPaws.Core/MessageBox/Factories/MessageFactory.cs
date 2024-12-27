using System.Text.Json;
using KindPaws.Core.MessageBox.Entities;
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.MessageBox.Factories;

public static class MessageFactory
{
    public static InBoxMessage CreateNewInBoxMessage<T>(T message)
        where T : IEvent
    {
        var outBoxMessageId = Guid.NewGuid();
        var outBoxMessageOccuredAt = DateTimeOffset.UtcNow;

        var payload = JsonSerializer.Serialize(message, System.Type.GetType(message.EventType)!);
        return new InBoxMessage(
            outBoxMessageId,
            message.EventType,
            payload,
            outBoxMessageOccuredAt,
            null,
            null);
    }

    public static InBoxMessage CreateInBoxMessage(
        Guid id,
        string type,
        string payload,
        DateTimeOffset occuredAt,
        DateTimeOffset? processedAt,
        string? error)
    {
        return new InBoxMessage(
            id,
            type,
            payload,
            occuredAt,
            processedAt,
            error);
    }
    
    public static OutBoxMessage CreateNewOutBoxMessage<T>(T message)
        where T : IEvent
    {
        var outBoxMessageId = Guid.NewGuid();
        var outBoxMessageOccuredAt = DateTimeOffset.UtcNow;

        var payload = JsonSerializer.Serialize(message, System.Type.GetType(message.EventType)!);
        return new OutBoxMessage(
            outBoxMessageId,
            message.EventType,
            payload,
            outBoxMessageOccuredAt,
            null,
            null);
    }

    public static OutBoxMessage CreateOutBoxMessage(
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