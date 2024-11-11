using KindPaws.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace KindPaws.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute permission)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.Sub)!.Value;

        if (userPermission is null)
            return;
        
        if (userPermission.Value == "Permissions")
            context.Succeed(permission);
        
    }
}