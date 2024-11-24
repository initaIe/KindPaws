using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(Guid RoleId) : ICommand;