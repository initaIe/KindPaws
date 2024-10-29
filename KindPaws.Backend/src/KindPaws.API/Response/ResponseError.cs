namespace KindPaws.API.Response;

public record ResponseError(
    string Code,
    string Message,
    string? InvalidPropertyName);