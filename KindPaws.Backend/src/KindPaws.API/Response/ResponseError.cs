namespace KindPaws.API.Response;

public record ResponseError(
    string? ErrorCode,
    string? ErrorMessage,
    string? InvalidPropertyName);