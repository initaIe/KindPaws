namespace KindPaws.Auth.Application.Common.Models;

public record AccessTokenParseResult(
    Guid AccountId,
    Guid Jti);