using KindPaws.Framework;
using KindPaws.Roles.Application.Features.Roles.Commands.CreateRole;
using KindPaws.Roles.Application.Features.Roles.Commands.DeleteRole;
using KindPaws.Roles.Contracts.Requests;
using KindPaws.Roles.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Roles.Presentation.Controllers;

public class RolesController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoleRequest request,
        [FromServices] CreateRoleHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{roleId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid roleId,
        [FromServices] DeleteRoleHandler handler,
        CancellationToken token)
    {
        var command = new DeleteRoleCommand(roleId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}