using FluentValidation;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Features.Auth.Commands.Register;
using KindPaws.Auth.Contracts.Responses;
using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.AccountsManagement.Entities;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Auth.Domain.Common;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly IValidator<LoginCommand> _commandValidator;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly ITokenProvider _tokenProvider;
    private readonly IAuthModuleOptionsProvider _authModuleOptionsProvider;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public LoginCommandHandler(
        IValidator<LoginCommand> commandValidator,
        IRepository<Account, AccountId> accountRepository,
        IAuthReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider,
        ILogger<RegisterCommandHandler> logger,
        ITokenProvider tokenProvider,
        IAuthModuleOptionsProvider authModuleOptionsProvider)
    {
        _commandValidator = commandValidator;
        _accountRepository = accountRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
        _logger = logger;
        _tokenProvider = tokenProvider;
        _authModuleOptionsProvider = authModuleOptionsProvider;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var accountByEmailAddress = await _readDbContext.Accounts.FirstOrDefaultAsync(
                a => a.EmailAddress == command.EmailAddress,
                cancellationToken);

            if (accountByEmailAddress == null)
                return ErrorsAuth.CredentialsAreInvalid().ToErrorList();

            var isPasswordValid = _passwordHashProvider.IsPasswordValid(
                accountByEmailAddress.PasswordHash,
                command.Password);

            if (!isPasswordValid)
                return ErrorsAuth.CredentialsAreInvalid().ToErrorList();

            var jti = Jti.CreateRandom();

            var expiresInDays = _authModuleOptionsProvider.GetRefreshSessionExpiresInDays();
            var expiresDateTimeOffset = DateTimeOffset.UtcNow.AddDays(expiresInDays);
            var expiresAt = RefreshSessionExpiresAt.Create(expiresDateTimeOffset).Value;

            var refreshSession = RefreshSession.CreateNew(jti, expiresAt);
            var accessToken = _tokenProvider.GenerateAccessToken(accountByEmailAddress.Id, jti.Value);
            var response = new LoginResponse(accessToken, refreshSession.RefreshToken.Value);

            var accountId = AccountId.Create(accountByEmailAddress.Id).Value;
            var getAccountResult = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
            getAccountResult.Value.AddRefreshSession(refreshSession);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            LogSuccess(accountId.Value);

            return response;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            var errorId = Guid.NewGuid();
            LogError(errorId, exception);
            return ErrorsAuth.LoginFailure(errorId).ToErrorList();
        }
    }

    private void LogSuccess(Guid accountId)
    {
        _logger.LogInformation(
            "Account with id {accountId} was logged in.",
            accountId);
    }

    private void LogError(Guid errorId, Exception exception)
    {
        _logger.LogError(
            "ErrorId: {errorId} | Failed to login | Exception: {exception}",
            errorId, exception);
    }
}