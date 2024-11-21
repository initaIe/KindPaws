using KindPaws.Accounts.Application.Features.Accounts.Commands.Create;
using KindPaws.Accounts.Application.Features.Accounts.Commands.Delete;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.Add;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.Delete;
using KindPaws.Accounts.Contracts;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Accounts.Presentation.Mappers;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Presentation.Contract;

public class AccountsContract : IAccountsContract
{
    private readonly ICommandHandler<Guid, CreateAccountCommand> _createAccountHandler;
    private readonly ICommandHandler<Guid, AddRefreshSessionCommand> _addRefreshSessionHandler;
    private readonly ICommandHandler<Guid, DeleteRefreshSessionCommand> _deleteRefreshSessionHandler;
    private readonly ICommandHandler<Guid, DeleteAccountCommand> _deleteAccountHandler;

    public AccountsContract(
        ICommandHandler<Guid, CreateAccountCommand> createAccountHandler,
        ICommandHandler<Guid, AddRefreshSessionCommand> addRefreshSessionHandler,
        ICommandHandler<Guid, DeleteRefreshSessionCommand> deleteRefreshSessionHandler,
        ICommandHandler<Guid, DeleteAccountCommand> deleteAccountHandler)
    {
        _createAccountHandler = createAccountHandler;
        _addRefreshSessionHandler = addRefreshSessionHandler;
        _deleteRefreshSessionHandler = deleteRefreshSessionHandler;
        _deleteAccountHandler = deleteAccountHandler;
    }

    public async Task<Result<Guid, ErrorList>> CreateAccountAsync(CreateAccountRequest request)
    {
        var command = request.ToCommand();
        return await _createAccountHandler.HandleAsync(command);
    }

    public async Task<Result<Guid, ErrorList>> AddRefreshSessionAsync(
        Guid accountId,
        AddRefreshSessionRequest request)
    {
        var command = request.ToCommand(accountId);
        return await _addRefreshSessionHandler.HandleAsync(command);
    }

    public async Task<Result<Guid, ErrorList>> DeleteRefreshSessionAsync(
        Guid accountId,
        Guid refreshSessionId)
    {
        var command = new DeleteRefreshSessionCommand(accountId, refreshSessionId);
        return await _deleteRefreshSessionHandler.HandleAsync(command);
    }

    public async Task<Result<Guid, ErrorList>> DeleteAccountAsync(Guid accountId)
    {
        var command = new DeleteAccountCommand(accountId);
        return await _deleteAccountHandler.HandleAsync(command);
    }
}