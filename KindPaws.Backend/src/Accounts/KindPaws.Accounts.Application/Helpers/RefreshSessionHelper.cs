using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Helpers;

public static class RefreshSessionHelper
{
    public static RefreshSession ForceCreateNewRefreshSession(
        Guid jti,
        DateTime expireTimestamp)
    {
        var refreshSessionJti = Jti.Create(jti).Value;
        var refreshSessionExpireTimestamp = RefreshSessionExpireTimestamp.Create(expireTimestamp).Value;
        
        return RefreshSession.CreateNew(refreshSessionJti, refreshSessionExpireTimestamp);
    }
}