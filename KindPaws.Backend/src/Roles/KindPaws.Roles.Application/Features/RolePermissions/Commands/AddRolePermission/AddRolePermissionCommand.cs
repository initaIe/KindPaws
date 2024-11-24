using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.RolePermissions.Commands.AddRolePermission;

public record AddRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId)
    : ICommand;