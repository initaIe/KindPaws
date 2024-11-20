using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Core.Abstractions;

public interface IRepository<TEntity, in TId>
    where TEntity : IEntity<TId> 
    where TId : IEquatable<TId>
{
    Task<Result<TEntity, Error>> GetByIdAsync(
        TId permissionId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    void Delete(TEntity entity);
}