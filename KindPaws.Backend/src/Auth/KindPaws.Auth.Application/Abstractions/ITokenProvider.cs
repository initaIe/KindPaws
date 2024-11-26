using KindPaws.Auth.Application.Models;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Auth.Application.Abstractions;

public interface ITokenProvider
{
    string GenerateAccessToken(Guid accountId, Guid jti);
    Result<AccessTokenParseResult, Error> ParseAccessToken(string token);
    Task<Result<Error>> ValidateAccessTokenAsync(string token);
}