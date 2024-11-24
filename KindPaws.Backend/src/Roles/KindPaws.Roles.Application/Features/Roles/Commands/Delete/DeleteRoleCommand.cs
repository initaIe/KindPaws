using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Commands.Delete;

public record DeleteRoleCommand(Guid RoleId) : ICommand;