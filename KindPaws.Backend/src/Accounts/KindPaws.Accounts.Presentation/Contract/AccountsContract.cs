using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.DataModels;
using KindPaws.Accounts.Application.Features.Accounts.Commands.CreateAccount;
using KindPaws.Accounts.Application.Features.Accounts.Commands.DeleteAccount;
using KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountByEmail;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.AddRefreshSession;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.DeleteRefreshSession;
using KindPaws.Accounts.Application.Features.RefreshSessions.Queries.GetRefreshSessionByAccountId;
using KindPaws.Accounts.Contracts;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Accounts.Presentation.Mappers;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Presentation.Contract;

public class AccountsContract : IAccountsContract
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly ICommandHandler<Guid, CreateAccountCommand> _createAccountHandler;
    private readonly ICommandHandler<Guid, AddRefreshSessionCommand> _addRefreshSessionByIdHandler;
    private readonly ICommandHandler<Guid, DeleteRefreshSessionCommand> _deleteRefreshSessionHandler;
    private readonly ICommandHandler<Guid, DeleteAccountCommand> _deleteAccountHandler;
    private readonly IQueryHandler<Result<Guid, ErrorList>, ValidateAccountByQuery> _validateAccountPasswordHandler;

    private readonly IQueryHandler<Result<RefreshSessionDataModel, ErrorList>, GetRefreshSessionByAccountIdQuery>
        _getRefreshSessionByAccountIdHandler;

    public AccountsContract(
        ICommandHandler<Guid, CreateAccountCommand> createAccountHandler,
        ICommandHandler<Guid, AddRefreshSessionCommand> addRefreshSessionByIdHandler,
        ICommandHandler<Guid, DeleteRefreshSessionCommand> deleteRefreshSessionHandler,
        ICommandHandler<Guid, DeleteAccountCommand> deleteAccountHandler,
        IAccountsReadDbContext dbContext,
        IQueryHandler<Result<Guid, ErrorList>, ValidateAccountByQuery> validateAccountPasswordHandler,
        IQueryHandler<Result<RefreshSessionDataModel, ErrorList>, GetRefreshSessionByAccountIdQuery>
            getRefreshSessionByAccountIdHandler)
    {
        _createAccountHandler = createAccountHandler;
        _addRefreshSessionByIdHandler = addRefreshSessionByIdHandler;
        _deleteRefreshSessionHandler = deleteRefreshSessionHandler;
        _deleteAccountHandler = deleteAccountHandler;
        _dbContext = dbContext;
        _validateAccountPasswordHandler = validateAccountPasswordHandler;
        _getRefreshSessionByAccountIdHandler = getRefreshSessionByAccountIdHandler;
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

    public async Task<bool> IsAccountByEmailAddressExists(
        string emailAddress,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Accounts.AnyAsync(
            a => a.EmailAddress == emailAddress,
            cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> ValidateAccountByEmailAsync(
        ValidateAccountByEmailAddressRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToCommand();
        return await _validateAccountPasswordHandler.HandleAsync(query, cancellationToken);
    }

    // public async Task<Result<RefreshSessionDataModel, ErrorList>> GetRefreshSessionByAccountId(
    //     Guid accountId,
    //     CancellationToken cancellationToken = default)
    // {
    //     var query = new GetRefreshSessionByAccountIdQuery(accountId);
    //     return await _getRefreshSessionByAccountIdHandler.HandleAsync(query, cancellationToken);
    // }

    // TODO: MOVE TO APPLICATION QUERIES
    public async Task<Result<IReadOnlyList<Guid>, ErrorList>> GetRolesAsync(
        Guid accountId,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(
            a => a.Id == accountId,
            cancellationToken);

        if (account == null)
            return ErrorsGeneral.RecordNotFound().ToErrorList();

        return account.Roles.ToList();
    }
}