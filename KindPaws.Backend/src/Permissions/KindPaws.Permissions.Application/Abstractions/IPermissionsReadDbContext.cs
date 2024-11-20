using KindPaws.Permissions.Contracts.Dtos;

namespace KindPaws.Permissions.Application.Abstractions;

public interface IPermissionsReadDbContext
{
    IQueryable<PermissionDto> Permissions { get; }
}