using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Managers;

public class RefreshSessionManager : IRefreshSessionManager
{
    private readonly AccountsWriteDbContext _dbContext;

    public RefreshSessionManager(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RefreshSession, Error>> GetByRefreshTokenAsync(
        Guid refreshToken, 
        CancellationToken cancellationToken = default)
    {
        var refreshSession =  await _dbContext.RefreshSessions.FirstOrDefaultAsync(
            rs => rs.RefreshToken == refreshToken,
            cancellationToken);

        if (refreshSession == null)
            return Errors.General.RecordNotFound(nameof(RefreshSession));

        return refreshSession;
    }
    
    // TODO:  refactor use UNITOFWORK
    public async Task DeleteAndSaveChangesAsync(
        RefreshSession refreshSession, 
        CancellationToken cancellationToken = default)
    {
        _dbContext.RefreshSessions.Remove(refreshSession);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}