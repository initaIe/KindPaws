namespace KindPaws.Accounts.Contracts.Requests;

public record RefreshTokensRequest(
    string AccessToken,
    Guid RefreshToken);