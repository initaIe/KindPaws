using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Features.AccountRoles.Commands.AddAccountRole;
using KindPaws.Accounts.Application.Features.AccountRoles.Commands.DeleteAccountRole;
using KindPaws.Accounts.Application.Features.Accounts.Commands.CreateAccount;
using KindPaws.Accounts.Application.Features.Accounts.Commands.DeleteAccount;
using KindPaws.Accounts.Application.Features.Accounts.Queries.GetAccountByEmailAddress;
using KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountPassword;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.AddRefreshSession;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.DeleteRefreshSession;
using KindPaws.Accounts.Contracts;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Accounts.Presentation.Mappers;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Presentation.Contract;

public class AccountsContract : IAccountsContract
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly ICommandHandler<Guid, CreateAccountCommand> _createAccountHandler;
    private readonly ICommandHandler<Guid, AddRefreshSessionCommand> _addRefreshSessionByIdHandler;
    private readonly ICommandHandler<Guid, DeleteRefreshSessionCommand> _deleteRefreshSessionHandler;
    private readonly ICommandHandler<Guid, DeleteAccountCommand> _deleteAccountHandler;
    private readonly ICommandHandler<Guid, AddAccountRoleCommand> _addAccountRoleHandler;
    private readonly ICommandHandler<Guid, DeleteAccountRoleCommand> _deleteAccountRoleHandler;
    private readonly IQueryHandler<Result<ErrorList>, ValidateAccountPasswordQuery> _validateAccountPasswordHandler;
    private readonly IQueryHandler<Result<AccountDto, ErrorList>, GetAccountByEmailAddressQuery> _getAccountByEmailAddressHandler;

    public AccountsContract(
        ICommandHandler<Guid, CreateAccountCommand> createAccountHandler,
        ICommandHandler<Guid, AddRefreshSessionCommand> addRefreshSessionByIdHandler,
        ICommandHandler<Guid, DeleteRefreshSessionCommand> deleteRefreshSessionHandler,
        ICommandHandler<Guid, DeleteAccountCommand> deleteAccountHandler,
        ICommandHandler<Guid, AddAccountRoleCommand> addAccountRoleHandler,
        ICommandHandler<Guid, DeleteAccountRoleCommand> deleteAccountRoleHandler, 
        IAccountsReadDbContext dbContext,
        IQueryHandler<Result<ErrorList>, ValidateAccountPasswordQuery> validateAccountPasswordHandler, 
        IQueryHandler<Result<AccountDto, ErrorList>, GetAccountByEmailAddressQuery> getAccountByEmailAddressHandler)
    {
        _createAccountHandler = createAccountHandler;
        _addRefreshSessionByIdHandler = addRefreshSessionByIdHandler;
        _deleteRefreshSessionHandler = deleteRefreshSessionHandler;
        _deleteAccountHandler = deleteAccountHandler;
        _addAccountRoleHandler = addAccountRoleHandler;
        _deleteAccountRoleHandler = deleteAccountRoleHandler;
        _dbContext = dbContext;
        _validateAccountPasswordHandler = validateAccountPasswordHandler;
        _getAccountByEmailAddressHandler = getAccountByEmailAddressHandler;
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
        return await _addRefreshSessionByIdHandler.HandleAsync(command, cancellationToken);
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

    public async Task<IReadOnlyList<AccountRoleDto>> GetAccountRolesByIdAsync(
        Guid accountId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.AccountRoles
            .Where(ar => ar.AccountId == accountId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> IsAccountByEmailAddressExists(
        string emailAddress, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Accounts.AnyAsync(
            a => a.EmailAddress == emailAddress, 
            cancellationToken);
    }

    public async Task<Result<ErrorList>> ValidateAccountPasswordAsync(
        ValidateAccountByEmailAddressRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToCommand();
        return await _validateAccountPasswordHandler.HandleAsync(query, cancellationToken);
    }

    public async Task<Result<AccountDto, ErrorList>> GetAccountByEmailAddressHandler(
        string emailAddress, 
        CancellationToken cancellationToken = default)
    {
        var query = new GetAccountByEmailAddressQuery(emailAddress);
        return await _getAccountByEmailAddressHandler.HandleAsync(query, cancellationToken);
    }
}