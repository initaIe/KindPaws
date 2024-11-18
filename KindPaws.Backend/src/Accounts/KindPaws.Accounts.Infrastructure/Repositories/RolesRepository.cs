using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Abstractions.Repositories;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly AccountsWriteDbContext _dbContext;

    public RolesRepository(AccountsWriteDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Result<Role, Error>> GetByIdAsync(
        Guid roleId,
        CancellationToken cancellationToken = default)
    {
        var role = await _dbContext.Roles.FirstOrDefaultAsync(
            u => u.Id == roleId,
            cancellationToken);

        if (role == null)
            return Errors.General.RecordNotFound(
                nameof(Role),
                "RoleId",
                roleId);

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