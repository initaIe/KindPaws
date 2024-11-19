using System.Security.Claims;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Application.Abstractions;

public interface ITokenProvider
{
    string GetAccessToken(
        string userId,
        string userEmail,
        string jti);

    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaimsAsync(
        string jwtAccessToken,
        CancellationToken cancellationToken = default);
}