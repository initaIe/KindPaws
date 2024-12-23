using KindPaws.Auth.Application.Models;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Application.Abstractions;

public interface ITokenProvider
{
    string GenerateAccessToken(Guid accountId, Guid jti);
    Result<AccessTokenParseResult, Error> ParseAccessToken(string token);
    Task<Result<Error>> ValidateAccessTokenWithoutLifeTimeAsync(string token);
}