using KindPaws.Permissions.Application.DataModels;

namespace KindPaws.Permissions.Application.Abstractions;

public interface IPermissionsReadDbContext
{
    IQueryable<PermissionDataModel> Permissions { get; }
}