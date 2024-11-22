using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Permissions.Create;

public record CreatePermissionCommand(string Code) : ICommand;