using FluentValidation;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Features.Auth.Commands.Register;
using KindPaws.Auth.Contracts.Responses;
using KindPaws.Auth.Domain;
using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly IValidator<LoginCommand> _commandValidator;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<RegisterHandler> _logger;

    public LoginHandler(
        IValidator<LoginCommand> commandValidator,
        IRepository<Account, AccountId> accountRepository,
        IAuthReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider,
        ILogger<RegisterHandler> logger,
        ITokenProvider tokenProvider)
    {
        _commandValidator = commandValidator;
        _accountRepository = accountRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var accountByEmailAddress = await _readDbContext.Accounts.FirstOrDefaultAsync(
                a => a.EmailAddress == command.EmailAddress,
                cancellationToken);

            if (accountByEmailAddress == null)
                return AuthErrors.CredentialsAreInvalid().ToErrorList();

            var isPasswordValid = _passwordHashProvider.IsPasswordValid(
                accountByEmailAddress.PasswordHash,
                command.Password);
            
            if (!isPasswordValid)
                return AuthErrors.CredentialsAreInvalid().ToErrorList();
            
            var jti = Jti.CreateRandom();
            var expiresAt = RefreshSessionExpiresAt.Create(DateTimeOffset.UtcNow.AddDays(60)).Value;
            
            var refreshSession = RefreshSession.CreateNew(jti, expiresAt);
            var accessToken = _tokenProvider.GenerateAccessToken(accountByEmailAddress.Id, jti.Value);
            var response = new LoginResponse(accessToken, refreshSession.RefreshToken.Value);
            
            var accountId = AccountId.Create(accountByEmailAddress.Id).Value;
            var getAccountResult = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
            getAccountResult.Value.AddRefreshSession(refreshSession);
            
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            
            SuccessLog(accountId.Value);

            return response;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            var errorId = Guid.NewGuid();
            
            ErrorLog(errorId, exception);
            
            return AuthErrors.LoginFailure(errorId).ToErrorList();
        }
    }
    
    private void SuccessLog(Guid accountId)
    {
        _logger.LogInformation("Account with id {accountId} was logged in.", accountId);
    }
    
    private void ErrorLog(Guid errorId, Exception exception)
    {
        _logger.LogError("ErrorId: {errorId} | Failed to login | Exception: {exception}", errorId, exception);
    }
}