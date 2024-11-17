using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Contracts.Responses;
using KindPaws.Accounts.Domain;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Application.Features.Commands.RefreshTokens;

public class RefreshTokensHandler : ICommandHandler<RefreshTokensResponse, RefreshTokensCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RefreshTokensHandler> _logger;

    public RefreshTokensHandler(
        IRefreshSessionManager refreshSessionManager,
        ITokenProvider tokenProvider,
        ILogger<RefreshTokensHandler> logger,
        UserManager<User> userManager)
    {
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<Result<RefreshTokensResponse, ErrorList>> HandleAsync(
        RefreshTokensCommand command,
        CancellationToken cancellationToken = default)
    {
        var refreshSessionResult = await _refreshSessionManager.GetByRefreshTokenAsync(
            command.RefreshToken,
            cancellationToken);

        if (refreshSessionResult.IsFailure)
            return refreshSessionResult.Error.ToErrorList();

        if (DateTime.UtcNow > refreshSessionResult.Value.ExpiresIn)
            return Errors.Accounts.ExpiredToken("RefreshToken").ToErrorList();

        var userClaimsResult = await _tokenProvider.GetUserClaimsAsync(
            command.AccessToken,
            cancellationToken);

        if (userClaimsResult.IsFailure)
            return userClaimsResult.Error.ToErrorList();

        var userIdString = userClaimsResult.Value.FirstOrDefault(c => c.Type == CustomClaims.Sub)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            return Errors.Accounts.TokenIsInvalid().ToErrorList();

        var user = await _userManager.FindByIdAsync(userIdString);

        if (user == null)
            return Errors.General.RecordNotFound(nameof(User)).ToErrorList();
        
        if (refreshSessionResult.Value.UserId != userId)
            return Errors.Accounts.TokenIsInvalid().ToErrorList();

        var userJtiString = userClaimsResult.Value.FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;

        if (!Guid.TryParse(userJtiString, out var jti))
            return Errors.Accounts.TokenIsInvalid().ToErrorList();

        if (jti != refreshSessionResult.Value.Jti)
            return Errors.Accounts.TokenIsInvalid().ToErrorList();

        await _refreshSessionManager.DeleteAndSaveChangesAsync(refreshSessionResult.Value, cancellationToken);

        var accessTokenResult = _tokenProvider.GenerateAccessToken(user);

        var refreshToken = await _tokenProvider.GenerateRefreshTokenAsync(
            user,
            accessTokenResult.Jti,
            cancellationToken);

        return new RefreshTokensResponse(accessTokenResult.AccessToken, refreshToken);
    }
}