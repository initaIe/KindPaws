using System.Data;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Auth.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AuthWriteWriteDbContext _writeDbContext;

    public UnitOfWork(AuthWriteWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default)
    {
        return await _writeDbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        return await _writeDbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }
}