using KindPaws.Roles.Application.Features.Roles.Create;
using KindPaws.Roles.Contracts.Requests;

namespace KindPaws.Roles.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateRoleCommand ToCommand(this CreateRoleRequest request)
        => new(request.Name);
}