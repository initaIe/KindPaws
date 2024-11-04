using System.Data;
using KindPaws.Core.Abstractions;
using KindPaws.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Species.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SpeciesWriteDbContext _dbContext;

    public UnitOfWork(SpeciesWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}