using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Features.Auth.Commands.Login;
using KindPaws.Accounts.Contracts.Responses;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Application.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly IRefreshTokenSettingsProvider _refreshTokenSettingsProvider;
    private readonly IUsersRepository _userRepository;
    private readonly IAccountsReadDbContext _dbContext;
    private readonly UserManager<Account> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        UserManager<Account> userManager,
        ITokenProvider tokenProvider,
        ILogger<LoginHandler> logger,
        IAccountsReadDbContext dbContext,
        IUsersRepository userRepository,
        IRefreshTokenSettingsProvider refreshTokenSettingsProvider)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
        _dbContext = dbContext;
        _userRepository = userRepository;
        _refreshTokenSettingsProvider = refreshTokenSettingsProvider;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var isUserByEmailExist = await _dbContext.Users.AnyAsync(u=>u.Email == command.Email, cancellationToken);
        if (!isUserByEmailExist)
            return Errors.Accounts.CredentialsAreInvalid().ToErrorList();

        var user = await _userRepository.GetByEmailAddressAsync(command.Email, cancellationToken);

        var isPasswordValid = await _userManager.CheckPasswordAsync(user.Value, command.Password);
        if (!isPasswordValid)
            return Errors.Accounts.CredentialsAreInvalid().ToErrorList();

        var accessTokenAndJti = _tokenProvider.GetAccessToken(user.Value.Id, user.Value.Email!);

        var refreshTokenExpiresInDays = _refreshTokenSettingsProvider.Get().ExpiresInDays;

        var refreshSession = RefreshSession.CreateNew(
            user.Value.Id,
            accessTokenAndJti.Jti,
            refreshTokenExpiresInDays);

        var loginResponse = new LoginResponse(accessTokenAndJti.AccessToken, refreshSession.Value.RefreshToken.Value);

        _logger.LogInformation("User with user name {UserName} logged in.", userByEmailExist.UserName);

        return loginResponse;
    }
}