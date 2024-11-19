using System.Security.Claims;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Application.Abstractions;

public interface ITokenProvider
{
    AccessTokenAndJti GetAccessTokenAndJti(
        Guid userId,
        string userEmail);

    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaimsAsync(
        string jwtAccessToken,
        CancellationToken cancellationToken = default);
}