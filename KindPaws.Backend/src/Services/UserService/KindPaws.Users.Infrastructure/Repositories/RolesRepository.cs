using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.RolesManagement.AggregateRoot;
using KindPaws.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Users.Infrastructure.Repositories;

public class RolesRepository : IRepository<Role, RoleId>
{
    private readonly UsersWriteDbContext _dbContext;

    public RolesRepository(UsersWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Role, Error>> GetByIdAsync(
        RoleId roleId,
        CancellationToken cancellationToken = default)
    {
        var role = await _dbContext.Roles
            .FirstOrDefaultAsync(
                r => r.Id == roleId,
                cancellationToken);

        if (role == null)
            return Errors.General.RecordNotFound(
                nameof(Role),
                nameof(RoleId),
                roleId.Value);

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