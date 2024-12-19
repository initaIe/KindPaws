using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.AccountsManagement.Events;
using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Auth.Application.Features.Accounts.EventHandlers;

public class AccountCreatedDomainEventHandler : IDomainEventHandler<AccountCreatedDomainEvent>
{
    private readonly IAuthModuleOptionsProvider _authModuleOptionsProvider;
    private readonly ILogger<AccountCreatedDomainEventHandler> _logger;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _authReadDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public AccountCreatedDomainEventHandler(
        IAuthReadDbContext authReadDbContext,
        IAuthModuleOptionsProvider authModuleOptionsProvider,
        ILogger<AccountCreatedDomainEventHandler> logger,
        IRepository<Account, AccountId> accountRepository,
        IUnitOfWork unitOfWork)
    {
        _authReadDbContext = authReadDbContext;
        _authModuleOptionsProvider = authModuleOptionsProvider;
        _logger = logger;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        AccountCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        // await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        //
        // try
        // {
        //     var defaultRoleName = _authModuleOptionsProvider.GetDefaultRoleName();
        //
        //     var defaultRoleId = await _authReadDbContext.Roles
        //         .Where(r => r.Name == defaultRoleName)
        //         .Select(r => r.Id)
        //         .FirstOrDefaultAsync(cancellationToken);
        //
        //     if (GuidValidator.IsEmpty(defaultRoleId))
        //     {
        //         LogCritical(Guid.NewGuid());
        //         throw new ApplicationException("Default role by name not found.");
        //     }
        //
        //     var accountRoleId = AccountRoleId.Create(defaultRoleId).Value;
        //     var accountId = AccountId.Create(domainEvent.AccountId).Value;
        //     
        //     var account = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
        //     account.Value.AddRole(accountRoleId);
        //
        //     await _unitOfWork.SaveChangesAsync(cancellationToken);
        //     await transaction.CommitAsync(cancellationToken);
        //     
        //     LogSuccess(account.Value.Id.Value);
        // }
        // catch (Exception exception)
        // {
        //     await transaction.RollbackAsync(cancellationToken);
        //     LogError(Guid.NewGuid(), exception);
        // }
    }
    private void LogSuccess(Guid accountId)
    {
        _logger.LogInformation(
            "[{name}] On account with id {accountId} was added default role after registration.",
            nameof(AccountCreatedDomainEventHandler),
            accountId);
    }

    private void LogError(Guid errorId, Exception exception)
    {
        _logger.LogError(
            "[{name}] ErrorId: {errorId} | Failed to add default role after registration | Exception: {exception}",
            nameof(AccountCreatedDomainEventHandler),
            errorId,
            exception);
    }
        
    private void LogCritical(Guid errorId)
    {
        _logger.LogError(
            "[{name}] ErrorId: {errorId} | Failed to add default role after registration",
            nameof(AccountCreatedDomainEventHandler),
            errorId);
    }
}