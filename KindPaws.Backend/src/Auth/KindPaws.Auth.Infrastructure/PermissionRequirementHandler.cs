using KindPaws.Framework.Authorization;
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
        var userIdString = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.Sub)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
        {
            context.Fail();
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<PermissionRequirementService>();

        var isAccountHasRequiredPermission = await service.HasRequiredPermission(userId, permission.Code);

        if (!isAccountHasRequiredPermission)
            context.Fail(new AuthorizationFailureReason(this, "Has no permission."));

        context.Succeed(permission);
    }
}