using KindPaws.Roles.Contracts.Dtos;

namespace KindPaws.Roles.Application.Abstractions;

public interface IRolesReadDbContext
{
    IQueryable<RoleDto> Roles { get; }
    IQueryable<RolePermissionDto> RolePermissions { get; }
}