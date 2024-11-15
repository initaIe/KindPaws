namespace KindPaws.Accounts.Contracts.Responses;

public record RefreshTokensResponse(
    string AccessToken,
    Guid RefreshToken);