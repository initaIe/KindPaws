using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Helpers;

public static class RefreshSessionHelper
{
    public static RefreshSession ForceCreateNewRefreshSession(
        Guid jti,
        DateTimeOffset expireTimestamp)
    {
        var refreshSessionJti = Jti.Create(jti).Value;
        var refreshSessionExpireTimestamp = RefreshSessionExpiresAt.Create(expireTimestamp).Value;
        
        return RefreshSession.CreateNew(refreshSessionJti, refreshSessionExpireTimestamp);
    }
}