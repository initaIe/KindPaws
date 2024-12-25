namespace KindPaws.Core.MessageBox.Entities;

public sealed class OutBoxMessage : BoxMessage
{
    private OutBoxMessage(
        Guid id,
        string type,
        string payload,
        DateTimeOffset occuredAt,
        DateTimeOffset? processedAt,
        string? error)
        : base(
            id,
            type,
            payload,
            occuredAt,
            processedAt
            , error)
    {
    }
}