using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Accounts.Infrastructure.Seeding.Configs;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Managers;

public class RolePermissionManager
{
    private readonly AccountsWriteDbContext _dbContext;

    public RolePermissionManager(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeIfNotExistsAsync(
        IEnumerable<RolePermissionDto> rolePermissionDtos,
        CancellationToken cancellationToken = default)
    {
        List<RolePermission> rolePermissionsToAdd = [];
        foreach (var rolePermissionDto in rolePermissionDtos)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(
                r => r.Name == rolePermissionDto.RoleName, cancellationToken);

            var permission = await _dbContext.Permissions.FirstOrDefaultAsync(
                r => r.Code == rolePermissionDto.PermissionCode, cancellationToken);

            var isRolePermissionExist = await _dbContext.RolePermissions.AnyAsync(
                rp => rp.RoleId == role!.Id && rp.PermissionId == permission!.Id,
                cancellationToken);

            if (!isRolePermissionExist)
                rolePermissionsToAdd.Add(new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = role!.Id,
                    PermissionId = permission!.Id
                });
        }

        await _dbContext.AddRangeAsync(rolePermissionsToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}