using FluentValidation;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Factories;
using KindPaws.Auth.Domain;
using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Register;

public class RegisterHandler : ICommandHandler<Guid, RegisterCommand>
{
    private readonly IValidator<RegisterCommand> _commandValidator;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly ILogger<RegisterHandler> _logger;

    public RegisterHandler(
        IValidator<RegisterCommand> commandValidator,
        IAuthReadDbContext readDbContext,
        IRepository<Account, AccountId> accountRepository,
        IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider,
        ILogger<RegisterHandler> logger)
    {
        _commandValidator = commandValidator;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        RegisterCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var isUsernameOrEmailAddressAlreadyTaken = await _readDbContext.Accounts.AnyAsync(
                a => a.UserName == command.UserName || a.EmailAddress == command.EmailAddress,
                cancellationToken);

            if (isUsernameOrEmailAddressAlreadyTaken)
                return GeneralErrors.RecordAlreadyExist(nameof(Account)).ToErrorList();

            var passwordHash = _passwordHashProvider.GenerateHash(command.Password);

            var account = AccountFactory.ForceCreateNew(command.UserName, command.EmailAddress, passwordHash);
            
            await _accountRepository.AddAsync(account, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
            
            SuccessLog(account.Id.Value);
            
            return account.Id.Value;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            var errorId = Guid.NewGuid();
            
            ErrorLog(errorId, exception);
            
            return AuthErrors.RegistrationFailure(errorId).ToErrorList();
        }
    }

    private void SuccessLog(Guid accountId)
    {
        _logger.LogInformation("Account with id {accountId} was registered.", accountId);
    }
    
    private void ErrorLog(Guid errorId, Exception exception)
    {
        _logger.LogError("ErrorId: {errorId} | Failed to register account | Exception: {exception}", errorId, exception);
    }
}