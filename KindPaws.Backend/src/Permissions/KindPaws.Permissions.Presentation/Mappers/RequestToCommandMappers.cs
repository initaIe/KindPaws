using KindPaws.Permissions.Application.Features.Permissions.Commands.Create;
using KindPaws.Permissions.Contracts.Requests;

namespace KindPaws.Permissions.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreatePermissionCommand ToCommand(this CreatePermissionRequest request)
        => new(request.Code);
}