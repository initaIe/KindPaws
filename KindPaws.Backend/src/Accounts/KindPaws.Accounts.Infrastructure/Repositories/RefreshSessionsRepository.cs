using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class RefreshSessionsRepository : IRepository<RefreshSession, RefreshSessionId>
{
    private readonly AccountsWriteDbContext _dbContext;

    public RefreshSessionsRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RefreshSession, Error>> GetByIdAsync(
        RefreshSessionId refreshSessionId,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = await _dbContext.RefreshSessions.FirstOrDefaultAsync(
            u => u.Id == refreshSessionId,
            cancellationToken);

        if (refreshSession == null)
            return Errors.General.RecordNotFound(
                nameof(RefreshSession),
                nameof(RefreshSessionId),
                refreshSessionId.Value);

        return refreshSession;
    }

    public async Task AddAsync(
        RefreshSession refreshSession,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.RefreshSessions.AddAsync(refreshSession, cancellationToken);
    }

    public void Delete(RefreshSession refreshSession)
    {
        _dbContext.RefreshSessions.Remove(refreshSession);
    }
}