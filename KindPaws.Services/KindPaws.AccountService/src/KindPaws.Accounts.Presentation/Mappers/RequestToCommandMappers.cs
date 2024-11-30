using KindPaws.Accounts.Application.Features.Accounts.Commands.CreateAccount;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.AddRefreshSession;
using KindPaws.Accounts.Contracts.Requests;

namespace KindPaws.Accounts.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateAccountCommand ToCommand(this CreateAccountRequest request)
        => new(request.UserName, request.EmailAddress, request.Password);

    public static AddRefreshSessionCommand ToCommand(
        this AddRefreshSessionRequest request,
        Guid accountId)
        => new(accountId, request.Jti);
}