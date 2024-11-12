using System.Data;

namespace KindPaws.Core.Abstractions.DataBase;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}