using KindPaws.Accounts.Contracts.Requests;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Contracts;

public interface IAccountsContract
{
    Task<Result<Guid, ErrorList>> CreateAccountAsync(CreateAccountRequest request);
    Task<Result<Guid, ErrorList>> AddRefreshSession(Guid accountId, AddRefreshSessionRequest request);
    Task<Result<Guid, ErrorList>> DeleteRefreshSession(Guid accountId, Guid refreshSessionId);
    Task<Result<Guid, ErrorList>> DeleteAccount(Guid accountId);
}