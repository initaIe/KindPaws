using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Repositories;

public class PermissionsRepository : IRepository<Permission, PermissionId>
{
    private readonly AuthWriteDbContext _dbContext;

    public PermissionsRepository(AuthWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Permission, Error>> GetByIdAsync(
        PermissionId permissionId,
        CancellationToken cancellationToken = default)
    {
        var role = await _dbContext.Permissions
            .FirstOrDefaultAsync(
                p => p.Id == permissionId,
                cancellationToken);

        if (role == null)
            return GeneralErrors.RecordNotFound(
                nameof(Permission),
                nameof(PermissionId),
                permissionId.Value);

        return role;
    }

    public async Task AddAsync(
        Permission permission, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Permissions.AddAsync(permission, cancellationToken);
    }

    public void Delete(Permission permission)
    {
        _dbContext.Permissions.Remove(permission);
    }
}

