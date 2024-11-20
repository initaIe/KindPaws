using KindPaws.Accounts.Application.Features.Commands.Create;
using KindPaws.Accounts.Contracts.Requests;

namespace KindPaws.Accounts.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateAccountCommand ToCommand(this CreateAccountRequest request)
        => new(request.UserName, request.EmailAddress, request.Password);
}