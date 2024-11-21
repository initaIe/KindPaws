using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Create;

public record CreateRoleCommand(string Name) : ICommand;