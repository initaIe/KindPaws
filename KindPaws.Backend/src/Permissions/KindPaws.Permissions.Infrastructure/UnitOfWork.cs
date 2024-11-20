using System.Data;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Permissions.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Permissions.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly PermissionsWriteDbContext _dbContext;

    public UnitOfWork(PermissionsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}