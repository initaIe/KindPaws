using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Roles.Create;

public record CreateRoleCommand(
    string Name)
    : ICommand;