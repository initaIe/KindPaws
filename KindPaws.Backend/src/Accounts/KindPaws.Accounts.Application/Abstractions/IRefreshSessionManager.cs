using KindPaws.Accounts.Domain;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshTokenAsync(
        Guid refreshToken,
        CancellationToken cancellationToken = default);

    Task DeleteAndSaveChangesAsync(
        RefreshSession refreshSession,
        CancellationToken cancellationToken = default);
}