using System.Data;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Accounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsWriteDbContext _dbContext;

    public UnitOfWork(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}