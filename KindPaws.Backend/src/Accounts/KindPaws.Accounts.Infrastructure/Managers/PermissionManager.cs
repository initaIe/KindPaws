using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Managers;

public class PermissionManager
{
    private readonly AccountsWriteDbContext _dbContext;

    public PermissionManager(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeIfByCodeNotExistsAsync(
        IEnumerable<Permission> permissions,
        CancellationToken cancellationToken = default)
    {
        List<Permission> permissionsToAdd = [];
        foreach (var permission in permissions)
        {
            var isPermissionByCodeExist = await _dbContext.Permissions
                .AnyAsync(p => p.Code == permission.Code, cancellationToken);

            if (!isPermissionByCodeExist)
                permissionsToAdd.Add(permission);
        }

        await _dbContext.Permissions.AddRangeAsync(permissionsToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<string>> GetUserPermissionCodesAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles
                .SelectMany(r => r.RolePermissions
                    .Select(rp => rp.Permission.Code)))
            .Distinct()
            .ToListAsync();
    }

    public async Task<bool> IsUserHavePermission(Guid userId, string permissionCode)
    {
        return await _dbContext.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles
                .SelectMany(r => r.RolePermissions
                    .Select(rp => rp.Permission.Code)))
            .AnyAsync(rp => rp == permissionCode);
    }
}