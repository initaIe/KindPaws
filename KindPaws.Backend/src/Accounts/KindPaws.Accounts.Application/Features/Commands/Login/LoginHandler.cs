using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Contracts.Responses;
using KindPaws.Accounts.Domain;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Application.Features.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        ILogger<LoginHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var userByEmailExist = await _userManager.FindByEmailAsync(command.Email);
        if (userByEmailExist == null)
            return Errors.Accounts.CredentialsAreInvalid().ToErrorList();

        var isPasswordValid = await _userManager.CheckPasswordAsync(userByEmailExist, command.Password);
        if (!isPasswordValid)
            return Errors.Accounts.CredentialsAreInvalid().ToErrorList();

        var accessToken = _tokenProvider.GenerateAccessToken(userByEmailExist);
        var refreshToken = await _tokenProvider.GenerateRefreshTokenAsync(
            userByEmailExist,
            accessToken.Jti,
            cancellationToken);

        var loginResponse = new LoginResponse(accessToken.AccessToken, refreshToken);

        _logger.LogInformation("User with user name {UserName} logged in.", userByEmailExist.UserName);

        return loginResponse;
    }
}