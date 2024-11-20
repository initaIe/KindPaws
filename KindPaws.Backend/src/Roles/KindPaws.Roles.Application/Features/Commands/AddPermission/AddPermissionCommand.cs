using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Commands.AddPermission;

public record AddPermissionCommand(
    Guid RoleId,
    Guid PermissionId)
    : ICommand;