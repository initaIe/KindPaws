using System.Data;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Volunteers.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly VolunteersWriteDbContext _dbContext;

    public UnitOfWork(VolunteersWriteDbContext dbContext)
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