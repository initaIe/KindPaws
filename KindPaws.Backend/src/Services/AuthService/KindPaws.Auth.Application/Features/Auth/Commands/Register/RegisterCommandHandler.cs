using FluentValidation;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Factories;
using KindPaws.Auth.Domain;
using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<Guid, RegisterCommand>
{
    private readonly IAuthModuleOptionsProvider _authModuleOptionsProvider;
    private readonly IValidator<RegisterCommand> _commandValidator;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly IAuthReadDbContext _authReadDbContext;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(
        IValidator<RegisterCommand> commandValidator,
        IAuthReadDbContext readDbContext,
        IRepository<Account, AccountId> accountRepository,
        IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider,
        ILogger<RegisterCommandHandler> logger,
        IAuthModuleOptionsProvider authModuleOptionsProvider,
        IAuthReadDbContext authReadDbContext)
    {
        _commandValidator = commandValidator;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
        _logger = logger;
        _authModuleOptionsProvider = authModuleOptionsProvider;
        _authReadDbContext = authReadDbContext;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        RegisterCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var isUsernameOrEmailAddressAlreadyTaken = await _readDbContext.Accounts.AnyAsync(
                a => a.UserName == command.UserName || a.EmailAddress == command.EmailAddress,
                cancellationToken);

            if (isUsernameOrEmailAddressAlreadyTaken)
                return ErrorsGeneral.RecordAlreadyExist(nameof(Account)).ToErrorList();

            var passwordHash = _passwordHashProvider.GenerateHash(command.Password);

            var defaultRoleName = _authModuleOptionsProvider.GetDefaultRoleName();
            
            var defaultRoleId = await _authReadDbContext.Roles
                .Where(r => r.Name == defaultRoleName)
                .Select(r => r.Id)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (GuidValidator.IsEmpty(defaultRoleId))
            {
                var ex = new ApplicationException("Default role by name not found.");
                LogCritical(Guid.NewGuid(), ex);
            }
            
            var defaultRole = AccountRoleId.Create(defaultRoleId).Value;

            var account = AccountFactory.ForceCreateNew(
                command.UserName, 
                command.EmailAddress,
                passwordHash, 
                defaultRole);

            await _accountRepository.AddAsync(account, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            LogSuccess(account.Id.Value);

            return account.Id.Value;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            var errorId = Guid.NewGuid();
            LogError(errorId, exception);
            return ErrorsAuth.RegistrationFailure(errorId).ToErrorList();
        }
    }

    private void LogSuccess(Guid accountId)
    {
        _logger.LogInformation(
            "[{name}] Account with id {accountId} was registered.",
            nameof(RegisterCommandHandler),
            accountId);
    }

    private void LogError(Guid errorId, Exception exception)
    {
        _logger.LogError(
            "[{name}] ErrorId: {errorId} | Failed to register account | Exception: {exception}",
            nameof(RegisterCommandHandler),
            errorId,
            exception);
    }
    
    private void LogCritical(Guid errorId, Exception exception)
    {
        _logger.LogError(
            "[{name}] ErrorId: {errorId} | Failed to add default role at registration | Exception: {exception}",
            nameof(RegisterCommandHandler),
            errorId,
            exception);
    }
}