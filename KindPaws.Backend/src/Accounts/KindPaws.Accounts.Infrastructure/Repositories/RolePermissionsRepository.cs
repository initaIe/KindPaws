using KindPaws.Accounts.Application.Abstractions.Repositories;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class RolePermissionsRepository: IRepository<RolePermission, RolePermissionId>
{
    private readonly AccountsWriteDbContext _dbContext;

    public RolePermissionsRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RolePermission, Error>> GetByIdAsync(
        RolePermissionId rolePermissionId, 
        CancellationToken cancellationToken = default)
    {
        var rolePermission = await _dbContext.RolePermissions.FirstOrDefaultAsync(
            u => u.Id == rolePermissionId, 
            cancellationToken);

        if (rolePermission == null)
            return Errors.General.RecordNotFound(
                nameof(RolePermission),
                nameof(RolePermissionId),
                rolePermissionId.Value);

        return rolePermission;
    }

    public async Task AddAsync(
        RolePermission rolePermission,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.RolePermissions.AddAsync(rolePermission, cancellationToken);
    }

    public void Delete(RolePermission rolePermission)
    {
        _dbContext.RolePermissions.Remove(rolePermission);
    }
}