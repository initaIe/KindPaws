using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Create;

public record CreatePermissionCommand(string Code) : ICommand;