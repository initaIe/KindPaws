using KindPaws.Users.Application.DataModels;

namespace KindPaws.Users.Application.Abstractions;

public interface IUsersReadDbContext
{
    IQueryable<UserDataModel> Users { get; }
    IQueryable<RoleDataModel> Roles { get; }
}