using FluentValidation;
using KindPaws.Auth.Application.Abstractions;
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

namespace KindPaws.Auth.Application.Features.Auth.Commands.RefreshTokens;

public class RefreshTokensHandler : ICommandHandler<RefreshTokensResponse, RefreshTokensCommand>
{
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _readDbContext;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RefreshTokensCommand> _validator;
    private readonly IAuthModuleOptionsProvider _authModuleOptionsProvider;
    private readonly ILogger<RefreshTokensHandler> _logger;


    public RefreshTokensHandler(
        IRepository<Account, AccountId> accountRepository,
        IValidator<RefreshTokensCommand> validator,
        ITokenProvider tokenProvider,
        IAuthReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IAuthModuleOptionsProvider authModuleOptionsProvider,
        ILogger<RefreshTokensHandler> logger)
    {
        _accountRepository = accountRepository;
        _validator = validator;
        _tokenProvider = tokenProvider;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _authModuleOptionsProvider = authModuleOptionsProvider;
        _logger = logger;
    }

    public async Task<Result<RefreshTokensResponse, ErrorList>> HandleAsync(
        RefreshTokensCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var accessTokenValidationResult =
                await _tokenProvider.ValidateAccessTokenWithoutLifeTimeAsync(command.AccessToken);
            if (accessTokenValidationResult.IsFailure)
                return accessTokenValidationResult.Error.ToErrorList();

            var accessTokenParseResult = _tokenProvider.ParseAccessToken(command.AccessToken);
            if (accessTokenParseResult.IsFailure)
                return accessTokenParseResult.Error.ToErrorList();

            var currentRefreshSessionDataModel = await _readDbContext.RefreshSessions
                .FirstOrDefaultAsync(rs =>
                        rs.AccountId == accessTokenParseResult.Value.AccountId
                        && rs.RefreshToken == command.RefreshToken
                        && rs.Jti == accessTokenParseResult.Value.Jti,
                    cancellationToken);

            if (currentRefreshSessionDataModel == null)
                return ErrorsAuth.TokenIsInvalid().ToErrorList();

            if (currentRefreshSessionDataModel.ExpiresAt < DateTimeOffset.UtcNow)
                return ErrorsAuth.ExpiredToken("RefreshToken").ToErrorList();

            var accountId = AccountId.Create(accessTokenParseResult.Value.AccountId).Value;
            var getAccountResult = await _accountRepository.GetByIdAsync(accountId, cancellationToken);

            var refreshSessionId = RefreshSessionId.Create(currentRefreshSessionDataModel.Id).Value;
            var currentRefreshSession = getAccountResult.Value.GetRefreshSessionById(refreshSessionId);

            var newJti = Jti.CreateRandom();
            var expiresInDays = _authModuleOptionsProvider.GetRefreshSessionExpiresInDays();
            var expiresAt = RefreshSessionExpiresAt.Create(expiresInDays).Value;

            var newAccessToken = _tokenProvider.GenerateAccessToken(getAccountResult.Value.Id.Value, newJti.Value);
            var newRefreshSession = RefreshSession.CreateNew(newJti, expiresAt);

            var response = new RefreshTokensResponse(newAccessToken, newRefreshSession.Id.Value);

            getAccountResult.Value.DeleteRefreshSession(currentRefreshSession.Value);
            getAccountResult.Value.AddRefreshSession(newRefreshSession);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            LogSuccess(getAccountResult.Value.Id.Value);

            return response;
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
            "[{name}] Account with id {accountId} refreshed tokens.",
            nameof(RefreshTokensHandler),
            accountId);
    }

    private void LogError(Guid errorId, Exception exception)
    {
        _logger.LogError(
            "[{name}] ErrorId: {errorId} | Failed to refresh tokens | Exception: {exception}",
            nameof(RefreshTokensHandler),
            errorId,
            exception);
    }
}