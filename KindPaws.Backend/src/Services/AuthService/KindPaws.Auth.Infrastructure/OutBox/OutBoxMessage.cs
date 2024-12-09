namespace KindPaws.Auth.Infrastructure.OutBox;

public class OutBoxMessage
{
    public Guid Id { get; init; }
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Type { get; init; } = null!;
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Payload { get; init; } = null!;
    public DateTimeOffset OccuredAt { get; init; }
    public DateTimeOffset? ProcessedAt { get; init; }
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? Error { get; init; }
}