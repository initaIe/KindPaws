namespace KindPaws.Core.MessageBox.Entities;

public interface IBoxMessage
{
    Guid Id { get; }
    string Type { get; }
    string Payload { get; }
    DateTimeOffset OccuredAt { get; }
    DateTimeOffset? ProcessedAt { get; }
    string? Error { get; }
}