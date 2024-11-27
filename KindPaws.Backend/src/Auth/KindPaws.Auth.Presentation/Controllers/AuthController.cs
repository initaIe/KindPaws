using KindPaws.Auth.Application.Features.Commands.Login;
using KindPaws.Auth.Application.Features.Commands.RefreshTokens;
using KindPaws.Auth.Application.Features.Commands.Register;
using KindPaws.Auth.Contracts.Requests;
using KindPaws.Auth.Presentation.Mappers;
using KindPaws.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Auth.Presentation.Controllers;

public class AuthController : ApplicationController
{
    [HttpPost("sessions")]
    public async Task<IActionResult> LogIn(
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
    
    [HttpPost("accounts")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("sessions/renewal-tokens")]
    public async Task<IActionResult> RefreshTokens(
        [FromBody] RefreshTokensRequest request,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}