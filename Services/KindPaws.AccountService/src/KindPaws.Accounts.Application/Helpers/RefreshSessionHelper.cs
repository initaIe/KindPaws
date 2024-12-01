using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Helpers;

public static class RefreshSessionHelper
{
    public static RefreshSession ForceCreateNewRefreshSession(
        Guid jti,
        DateTimeOffset expiresAt)
    {
        var refreshSessionJti = Jti.Create(jti).Value;
        var refreshSessionExpiresAt = RefreshSessionExpiresAt.Create(expiresAt).Value;

        return RefreshSession.CreateNew(refreshSessionJti, refreshSessionExpiresAt);
    }
}