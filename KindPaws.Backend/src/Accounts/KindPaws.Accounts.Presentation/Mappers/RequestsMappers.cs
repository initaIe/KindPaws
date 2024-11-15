using KindPaws.Accounts.Application.Features.Commands.Login;
using KindPaws.Accounts.Application.Features.Commands.RefreshTokens;
using KindPaws.Accounts.Application.Features.Commands.Register;
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