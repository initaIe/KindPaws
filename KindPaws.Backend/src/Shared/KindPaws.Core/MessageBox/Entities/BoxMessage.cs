using System.Text.Json;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.MessageBox.Entities;

public class BoxMessage : IBoxMessage
{
    protected BoxMessage(
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

    public Guid Id { get; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Type { get; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Payload { get; }
    public DateTimeOffset OccuredAt { get; }
    public DateTimeOffset? ProcessedAt { get; set; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? Error { get; set; }
}