using KindPaws.Roles.Application.Features.RolePermissions.Commands.AddRolePermission;
using KindPaws.Roles.Application.Features.Roles.Commands.CreateRole;
using KindPaws.Roles.Contracts.Requests;

namespace KindPaws.Roles.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateRoleCommand ToCommand(this CreateRoleRequest request)
        => new(request.Name);

    public static AddRolePermissionCommand ToCommand(this AddRolePermissionRequest request, Guid roleId)
        => new(roleId, request.PermissionId);
}