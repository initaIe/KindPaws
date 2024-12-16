using KindPaws.Auth.Application.Features.Auth.Commands.Login;
using KindPaws.Auth.Application.Features.Auth.Commands.RefreshTokens;
using KindPaws.Auth.Application.Features.Auth.Commands.Register;
using KindPaws.Auth.Contracts.Requests;

namespace KindPaws.Auth.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static LoginCommand ToCommand(this LoginRequest request)
        => new(request.EmailAddress, request.Password);

    public static RegisterCommand ToCommand(this RegisterRequest request)
        => new(request.Username, request.EmailAddress, request.Password);

    public static RefreshTokensCommand ToCommand(this RefreshTokensRequest request)
        => new(request.AccessToken, request.RefreshToken);
}