namespace KindPaws.Auth.Application.Abstractions;

public interface ITokenProvider
{
    string GenerateAccessToken(Guid accountId, Guid jti);
}