using KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountByEmail;
using KindPaws.Accounts.Contracts.Requests;

namespace KindPaws.Accounts.Presentation.Mappers;

public static class RequestToQueryMappers
{
    public static ValidateAccountByQuery ToCommand(
        this ValidateAccountByEmailAddressRequest request)
        => new(request.EmailAddress, request.Password);
}