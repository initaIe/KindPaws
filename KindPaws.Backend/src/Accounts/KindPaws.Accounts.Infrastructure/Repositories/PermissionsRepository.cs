using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Abstractions.Repositories;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class PermissionsRepository : IRepository<Permission, PermissionId>
{
    private readonly AccountsWriteDbContext _dbContext;

    public PermissionsRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Permission, Error>> GetByIdAsync(
        PermissionId permissionId,
        CancellationToken cancellationToken = default)
    {
        var permission = await _dbContext.Permissions.FirstOrDefaultAsync(
            u => u.Id == permissionId, 
            cancellationToken);

        if (permission == null)
            return Errors.General.RecordNotFound(
                nameof(User),
                nameof(PermissionId),
                permissionId.Value);

        return permission;
    }

    public async Task AddAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await _dbContext.Permissions.AddAsync(permission, cancellationToken);
    }

    public void Delete(Permission permission)
    {
        _dbContext.Permissions.Remove(permission);
    }
}