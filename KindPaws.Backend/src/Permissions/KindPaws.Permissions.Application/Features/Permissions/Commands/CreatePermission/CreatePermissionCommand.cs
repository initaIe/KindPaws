using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Permissions.Commands.CreatePermission;

public record CreatePermissionCommand(string Code) : ICommand;