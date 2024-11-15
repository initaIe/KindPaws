using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Infrastructure.Managers;
using KindPaws.Framework.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.Services;

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
        var permissionManager = scope.ServiceProvider.GetRequiredService<PermissionManager>();

        var userIdString = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.Sub)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            context.Fail();
            return;
        }
        
        var isUserHavePermission = await permissionManager.IsUserHavePermission(userId, permission.Code);
        
        if (isUserHavePermission)
        {
            context.Succeed(permission);
            return;
        }

        context.Fail();
    }
}