using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.Persistence.DbContexts;
using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Repositories;

public class RolesRepository : IRepository<Role, AccountRoleId>
{
    private readonly AuthWriteDbContext _writeDbContext;

    public RolesRepository(AuthWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<Result<Role, Error>> GetByIdAsync(
        AccountRoleId accountRoleId,
        CancellationToken cancellationToken = default)
    {
        var role = await _writeDbContext.Roles
            .FirstOrDefaultAsync(
                role => role.Id == accountRoleId,
                cancellationToken);

        if (role == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(Role),
                nameof(AccountRoleId),
                accountRoleId.Value);

        return role;
    }

    public async Task AddAsync(
        Role role,
        CancellationToken cancellationToken = default)
    {
        await _writeDbContext.Roles.AddAsync(role, cancellationToken);
    }

    public void Delete(Role role)
    {
        _writeDbContext.Roles.Remove(role);
    }
}