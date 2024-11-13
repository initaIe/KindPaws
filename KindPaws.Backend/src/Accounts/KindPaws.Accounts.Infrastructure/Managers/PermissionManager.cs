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

    public async Task AddRangeIfNotExistsAsync(
        IEnumerable<string> permissionCodes,
        CancellationToken cancellationToken = default)
    {
        List<Permission> permissionsToAdd = [];
        foreach (var permissionCode in permissionCodes)
        {
            var isPermissionExist = await _dbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode, cancellationToken);

            if (!isPermissionExist)
                permissionsToAdd.Add(new Permission { Code = permissionCode });
        }

        await _dbContext.Permissions.AddRangeAsync(permissionsToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}