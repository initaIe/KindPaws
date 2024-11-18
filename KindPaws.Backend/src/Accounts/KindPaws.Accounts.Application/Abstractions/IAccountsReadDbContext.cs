using KindPaws.Accounts.Domain.Entities;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<RolePermission> RolePermissions { get; }
    IQueryable<Permission> Permissions { get; }
    IQueryable<RefreshSession> RefreshSessions { get; }
    IQueryable<User> Users { get; }
    IQueryable<Role> Roles { get; }
}