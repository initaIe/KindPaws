using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Permissions.Commands.Delete;

public record DeletePermissionCommand(Guid PermissionId) : ICommand;