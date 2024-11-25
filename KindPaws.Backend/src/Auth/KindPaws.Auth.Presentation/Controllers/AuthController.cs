using KindPaws.Auth.Application.Features.Login;
using KindPaws.Auth.Contracts.Requests;
using KindPaws.Auth.Presentation.Mappers;
using KindPaws.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Auth.Presentation.Controllers;

public class AuthController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Login(
        [FromBody] LoginByEmailAddressRequest request,
        [FromServices] LoginByEmailAddressHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}