using KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountPassword;
using KindPaws.Accounts.Contracts.Requests;

namespace KindPaws.Accounts.Presentation.Mappers;

public static class RequestToQueryMappers
{
    public static ValidateAccountPasswordQuery ToCommand(
        this ValidateAccountByEmailAddressRequest request)
        => new(request.EmailAddress, request.Password);
}