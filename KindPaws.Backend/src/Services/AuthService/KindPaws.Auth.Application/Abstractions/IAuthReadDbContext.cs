using KindPaws.Auth.Application.DataModels;

namespace KindPaws.Auth.Application.Abstractions;

public interface IAuthReadDbContext
{
    IQueryable<AccountDataModel> Accounts { get; }
    IQueryable<RoleDataModel> Roles { get; }
    IQueryable<PermissionDataModel> Permissions { get; }
}