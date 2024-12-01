using KindPaws.Auth.Application.Features.Commands.Login;
using KindPaws.Auth.Application.Features.Commands.RefreshTokens;
using KindPaws.Auth.Application.Features.Commands.Register;
using KindPaws.Auth.Contracts.Requests;

namespace KindPaws.Auth.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static LoginByEmailAddressCommand ToCommand(this LoginByEmailAddressRequest request)
        => new(request.EmailAddress, request.Password);

    public static RegisterCommand ToCommand(this RegisterRequest request)
        => new(request.UserName, request.EmailAddress, request.Password);

    public static RefreshTokensCommand ToCommand(this RefreshTokensRequest request)
        => new(request.AccessToken, request.RefreshToken);
}