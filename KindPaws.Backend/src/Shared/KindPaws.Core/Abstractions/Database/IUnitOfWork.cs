using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Core.Abstractions.Database;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}