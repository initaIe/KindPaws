using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Contracts;

public interface IAccountsContract
{
    Task<Result<Guid, ErrorList>> CreateAccountAsync(
        CreateAccountRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> AddRefreshSessionAsync(
        Guid accountId,
        AddRefreshSessionRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> DeleteRefreshSessionAsync(
        Guid accountId,
        Guid refreshSessionId,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> DeleteAccountAsync(
        Guid accountId,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> AddAccountRoleAsync(
        Guid accountId,
        AddAccountRoleRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> DeleteAccountRoleAsync(
        Guid accountId,
        Guid accountRoleId,
        CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<AccountRoleDto>> GetAccountRolesByIdAsync(
        Guid accountId,
        CancellationToken cancellationToken = default);
    
    Task<bool> IsAccountByEmailAddressExists(
        string emailAddress,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> ValidateAccountByEmailAsync(
        ValidateAccountByEmailAddressRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<RefreshSessionDto, ErrorList>> GetRefreshSessionByAccountId(
        Guid accountId,
        CancellationToken cancellationToken = default);
}