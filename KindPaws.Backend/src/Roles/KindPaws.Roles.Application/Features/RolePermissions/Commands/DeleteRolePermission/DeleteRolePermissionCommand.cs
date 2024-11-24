using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.RolePermissions.Commands.DeleteRolePermission;

public record DeleteRolePermissionCommand(
    Guid RoleId,
    Guid RolePermissionId)
    : ICommand;