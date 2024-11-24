using KindPaws.Framework;
using KindPaws.Permissions.Application.Features.Permissions.Commands.Create;
using KindPaws.Permissions.Application.Features.Permissions.Commands.Delete;
using KindPaws.Permissions.Contracts.Requests;
using KindPaws.Permissions.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Permissions.Presentation.Controllers;

public class PermissionsController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePermissionRequest request,
        [FromServices] CreatePermissionHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{permissionId:guid}")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid permissionId,
        [FromServices] DeletePermissionHandler handler,
        CancellationToken token)
    {
        var command = new DeletePermissionCommand(permissionId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}