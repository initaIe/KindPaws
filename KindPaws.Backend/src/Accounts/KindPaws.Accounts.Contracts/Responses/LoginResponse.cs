namespace KindPaws.Accounts.Contracts.Responses;

public record LoginResponse(
    string AccessToken,
    Guid RefreshToken);