using KindPaws.Auth.Application.Common.DataModels;

namespace KindPaws.Auth.Application.Abstractions;

public interface IAuthReadDbContext
{
    IQueryable<AccountDataModel> Accounts { get; }
    IQueryable<RefreshSessionDataModel> RefreshSessions { get; }
    IQueryable<RoleDataModel> Roles { get; }
    IQueryable<PermissionDataModel> Permissions { get; }
}