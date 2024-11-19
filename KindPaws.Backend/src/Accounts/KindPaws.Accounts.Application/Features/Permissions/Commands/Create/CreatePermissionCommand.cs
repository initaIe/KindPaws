using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Permissions.Commands.Create;

public record CreatePermissionCommand(
    string Code)
    : ICommand;