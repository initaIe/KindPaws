using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Roles.AddPermission;

public record AddPermissionCommand(
    Guid RoleId,
    Guid PermissionId)
    : ICommand;