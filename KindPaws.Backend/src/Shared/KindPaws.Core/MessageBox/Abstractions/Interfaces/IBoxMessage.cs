namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IBoxMessage
{
    Guid Id { get; }
    string Type { get; }
    string Payload { get; }
    DateTimeOffset OccuredAt { get; }
    DateTimeOffset? ProcessedAt { get; }
    string? Error { get; }
}