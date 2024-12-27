using System.Text.Json;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.MessageBox.Entities;

public sealed class OutBoxMessage : BoxMessage, IOutBoxMessage
{
    public OutBoxMessage(
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
            processedAt,
            error)
    {
    }
}