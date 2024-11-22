using KindPaws.Accounts.Application.Features.AccountRoles.Commands.Add;
using KindPaws.Accounts.Application.Features.AccountRoles.Commands.Delete;
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
    private readonly ICommandHandler<Guid, AddAccountRoleCommand> _addAccountRoleHandler;
    private readonly ICommandHandler<Guid, DeleteAccountRoleCommand> _deleteAccountRoleHandler;

    public AccountsContract(
        ICommandHandler<Guid, CreateAccountCommand> createAccountHandler,
        ICommandHandler<Guid, AddRefreshSessionCommand> addRefreshSessionHandler,
        ICommandHandler<Guid, DeleteRefreshSessionCommand> deleteRefreshSessionHandler,
        ICommandHandler<Guid, DeleteAccountCommand> deleteAccountHandler,
        ICommandHandler<Guid, AddAccountRoleCommand> addAccountRoleHandler, 
        ICommandHandler<Guid, DeleteAccountRoleCommand> deleteAccountRoleHandler)
    {
        _createAccountHandler = createAccountHandler;
        _addRefreshSessionHandler = addRefreshSessionHandler;
        _deleteRefreshSessionHandler = deleteRefreshSessionHandler;
        _deleteAccountHandler = deleteAccountHandler;
        _addAccountRoleHandler = addAccountRoleHandler;
        _deleteAccountRoleHandler = deleteAccountRoleHandler;
    }

    public async Task<Result<Guid, ErrorList>> CreateAccountAsync(
        CreateAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        return await _createAccountHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> AddRefreshSessionAsync(
        Guid accountId,
        AddRefreshSessionRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(accountId);
        return await _addRefreshSessionHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> DeleteRefreshSessionAsync(
        Guid accountId,
        Guid refreshSessionId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteRefreshSessionCommand(accountId, refreshSessionId);
        return await _deleteRefreshSessionHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> DeleteAccountAsync(
        Guid accountId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteAccountCommand(accountId);
        return await _deleteAccountHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> AddAccountRoleAsync(
        Guid accountId,
        AddAccountRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(accountId);
        return await _addAccountRoleHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> DeleteAccountRoleAsync(
        Guid accountId,
        Guid accountRoleId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteAccountRoleCommand(accountId, accountRoleId);
        return await _deleteAccountRoleHandler.HandleAsync(command, cancellationToken);
    }
}