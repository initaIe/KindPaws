using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Roles.Application.Features.Roles.Queries.GetIdByName;

public record GetRoleIdByNameQuery(string RoleName) : IQuery;