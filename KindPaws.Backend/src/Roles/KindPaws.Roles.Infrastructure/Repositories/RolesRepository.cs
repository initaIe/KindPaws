using KindPaws.Core.Abstractions;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Roles.Infrastructure.Repositories;

public class RolesRepository : IRepository<Role, RoleId>
{
    private readonly RolesWriteDbContext _dbContext;

    public RolesRepository(RolesWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<Role, Error>> GetByIdAsync(
        RoleId permissionId,
        CancellationToken cancellationToken = default)
    {
        var role = await _dbContext.Roles.FirstOrDefaultAsync(
            u => u.Id == permissionId,
            cancellationToken);

        if (role == null)
            return Errors.General.RecordNotFound(
                nameof(Role),
                nameof(RoleId),
                permissionId.Value);

        return role;
    }

    public async Task AddAsync(
        Role role,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(role, cancellationToken);
    }

    public void Delete(Role role)
    {
        _dbContext.Roles.Remove(role);
    }
}