using KindPaws.Accounts.Contracts;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Models;
using KindPaws.Auth.Contracts.Responses;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Application.Features.Login;

public class LoginByEmailAddressHandler : ICommandHandler<LoginResponse, LoginByEmailAddressCommand>
{
    private readonly IAccountsContract _accountsContract;
    private readonly ITokenProvider _tokenProvider;

    public LoginByEmailAddressHandler(
        IAccountsContract accountsContract, 
        ITokenProvider tokenProvider)
    {
        _accountsContract = accountsContract;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginByEmailAddressCommand command,
        CancellationToken cancellationToken = default)
    {
        var validateAccountPasswordRequest = new ValidateAccountByEmailAddressRequest(command.EmailAddress, command.Password);
        var accountValidationResult = await _accountsContract.ValidateAccountByEmailAsync(
            validateAccountPasswordRequest,
            cancellationToken);

        if (accountValidationResult.IsFailure)
            return Errors.Auth.CredentialsAreInvalid().ToErrorList();

        var jti = Jti.CreateRandom();

        var addRefreshSessionRequest = new AddRefreshSessionRequest(jti.Value);
        var addRefreshSessionResult = await _accountsContract.AddRefreshSessionAsync(
            accountValidationResult.Value,
            addRefreshSessionRequest,
            cancellationToken);

        if (addRefreshSessionResult.IsFailure)
            return addRefreshSessionResult.Error;

        var accessToken = _tokenProvider.GenerateAccessToken(accountValidationResult.Value, jti.Value);

        return new LoginResponse(accessToken, addRefreshSessionResult.Value);
    }
}