using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace KindPaws.Core.Abstractions.DataBase;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}