using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Permissions.Commands.DeletePermission;

public record DeletePermissionCommand(Guid PermissionId) : ICommand;