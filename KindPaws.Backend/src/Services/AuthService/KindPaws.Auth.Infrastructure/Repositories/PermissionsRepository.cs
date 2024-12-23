using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Repositories;

public class PermissionsRepository : IRepository<Permission, PermissionId>
{
    private readonly AuthWriteWriteDbContext _writeDbContext;

    public PermissionsRepository(AuthWriteWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<Result<Permission, Error>> GetByIdAsync(
        PermissionId permissionId,
        CancellationToken cancellationToken = default)
    {
        var permission = await _writeDbContext.Permissions
            .FirstOrDefaultAsync(
                permission => permission.Id == permissionId,
                cancellationToken);

        if (permission == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(Permission),
                nameof(PermissionId),
                permissionId.Value);

        return permission;
    }

    public async Task AddAsync(
        Permission permission,
        CancellationToken cancellationToken = default)
    {
        await _writeDbContext.Permissions.AddAsync(permission, cancellationToken);
    }

    public void Delete(Permission permission)
    {
        _writeDbContext.Permissions.Remove(permission);
    }
}