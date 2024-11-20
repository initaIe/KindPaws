using KindPaws.Accounts.Application.Commands.RefreshTokens;
using KindPaws.Accounts.Application.Commands.Register;
using KindPaws.Accounts.Application.Features.Auth.Commands.Login;
using KindPaws.Accounts.Contracts.Requests;

namespace KindPaws.Accounts.Presentation.Mappers;

public static class RequestsMappers
{
    public static RegisterCommand ToCommand(this RegisterRequest request)
        => new(
            request.Email,
            request.UserName,
            request.Password);

    public static LoginCommand ToCommand(this LoginRequest request)
        => new(
            request.Email,
            request.Password);

    public static RefreshTokensCommand ToCommand(this RefreshTokensRequest request)
        => new(
            request.AccessToken,
            request.RefreshToken);
}