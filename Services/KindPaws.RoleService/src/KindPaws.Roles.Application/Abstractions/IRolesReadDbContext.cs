using KindPaws.Roles.Application.DataModels;

namespace KindPaws.Roles.Application.Abstractions;

public interface IRolesReadDbContext
{
    IQueryable<RoleDataModel> Roles { get; }
}