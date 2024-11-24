using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Permissions.Application.Features.Permissions.Queries.GetIdByName;

public record GetPermissionIdByCodeQuery(string PermissionCode) : IQuery;