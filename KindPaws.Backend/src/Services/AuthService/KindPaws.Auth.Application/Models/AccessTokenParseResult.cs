namespace KindPaws.Auth.Application.Models;

public record AccessTokenParseResult(
    Guid AccountId,
    Guid Jti);