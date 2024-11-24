using KindPaws.Accounts.Contracts;
using KindPaws.Framework.Authorization;
using KindPaws.Permissions.Contracts;
using KindPaws.Roles.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute permission)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var accountsContract = scope.ServiceProvider.GetRequiredService<IAccountsContract>();
        var rolesContract = scope.ServiceProvider.GetRequiredService<IRolesContract>();
        var permissionsContract = scope.ServiceProvider.GetRequiredService<IPermissionsContract>();
        
        var userIdString = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.Sub)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            context.Fail();
            return;
        }

        // var isUserHavePermission = await permissionManager.HasUserPermission(userId, permission.Code);
        //
        // if (isUserHavePermission)
        // {
        //     context.Succeed(permission);
        //     return;
        // }

        context.Fail();
    }
}