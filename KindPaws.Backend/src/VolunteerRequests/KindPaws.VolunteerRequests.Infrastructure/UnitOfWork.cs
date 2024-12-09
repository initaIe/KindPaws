using System.Data;
using KindPaws.Core.Abstractions.Database;
using KindPaws.VolunteerRequests.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.VolunteerRequests.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly VolunteerRequestsWriteDbContext _dbContext;

    public UnitOfWork(VolunteerRequestsWriteDbContext dbContext)
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