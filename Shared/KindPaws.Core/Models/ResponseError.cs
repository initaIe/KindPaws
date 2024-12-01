namespace KindPaws.Core.Models;

public record ResponseError(
    string Code,
    string Message,
    string? InvalidPropertyName);