using KindPaws.Accounts.Contracts.Dtos;

namespace KindPaws.Permissions.Application.Abstractions;

public interface IPermissionsReadDbContext
{
    IQueryable<PermissionDto> Permissions { get; }
}