using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.RolePermissions.Add;

public record AddRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId)
    : ICommand;