using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Commands.Create;

public record CreateRoleCommand(string Name) : ICommand;