using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Permissions.Delete;

public record DeletePermissionCommand(Guid PermissionId) : ICommand;