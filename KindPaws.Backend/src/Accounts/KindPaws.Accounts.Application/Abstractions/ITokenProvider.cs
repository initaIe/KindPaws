using System.Security.Claims;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Domain;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Application.Abstractions;

public interface ITokenProvider
{
    JwtAccessTokenCreationResult GenerateAccessToken(User user);
    Task<Guid> GenerateRefreshTokenAsync(
        User user, 
        Guid jti,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaimsAsync(
        string jwtAccessToken,
        CancellationToken cancellationToken = default);
}