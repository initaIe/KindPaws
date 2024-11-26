namespace KindPaws.Auth.Contracts.Requests;

public record RefreshTokensRequest(
    string AccessToken,
    Guid RefreshToken);