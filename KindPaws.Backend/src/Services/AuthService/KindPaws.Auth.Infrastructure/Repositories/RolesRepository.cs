using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Repositories;

public class RolesRepository : IRepository<Role, AccountRoleId>
{
    private readonly AuthWriteDbContext _dbContext;

    public RolesRepository(AuthWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Role, Error>> GetByIdAsync(
        AccountRoleId accountRoleId,
        CancellationToken cancellationToken = default)
    {
        var role = await _dbContext.Roles
            .FirstOrDefaultAsync(
                v => v.Id == accountRoleId,
                cancellationToken);

        if (role == null)
            return GeneralErrors.General.RecordNotFound(
                nameof(Role),
                nameof(AccountRoleId),
                accountRoleId.Value);

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

