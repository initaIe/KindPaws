using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Delete;

public record DeleteRoleCommand(Guid RoleId) : ICommand;