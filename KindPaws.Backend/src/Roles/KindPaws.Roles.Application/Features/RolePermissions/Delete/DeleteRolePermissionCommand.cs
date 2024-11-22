using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.RolePermissions.Delete;

public record DeleteRolePermissionCommand(
    Guid RoleId,
    Guid RolePermissionId) 
    : ICommand;