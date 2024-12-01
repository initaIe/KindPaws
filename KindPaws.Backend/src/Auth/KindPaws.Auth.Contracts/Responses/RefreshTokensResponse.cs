namespace KindPaws.Auth.Contracts.Responses;

public record RefreshTokensResponse(
    string AccessToken,
    Guid RefreshToken);