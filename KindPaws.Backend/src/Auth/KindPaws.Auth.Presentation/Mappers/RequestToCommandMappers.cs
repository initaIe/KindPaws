using KindPaws.Auth.Application.Features.Login;
using KindPaws.Auth.Contracts.Requests;

namespace KindPaws.Auth.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static LoginByEmailAddressCommand ToCommand(this LoginByEmailAddressRequest request)
        => new(request.EmailAddress, request.Password);
}