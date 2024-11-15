namespace KindPaws.Accounts.Application.Models;

public record JwtAccessTokenCreationResult(
    string AccessToken,
    Guid Jti);