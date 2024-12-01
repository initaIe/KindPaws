using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(string Name) : ICommand;