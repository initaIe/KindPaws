using KindPaws.Accounts.Application.Features.Commands.Login;
using KindPaws.Accounts.Application.Features.Commands.RefreshTokens;
using KindPaws.Accounts.Application.Features.Commands.Register;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Accounts.Presentation.Mappers;
using KindPaws.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromServices] RegisterHandler handler,
        [FromBody] RegisterRequest request)
    {
        var command = request.ToCommand();
        var result = await handler.HandleAsync(command);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("logination")]
    public async Task<IActionResult> Login(
        [FromServices] LoginHandler handler,
        [FromBody] LoginRequest request)
    {
        var command = request.ToCommand();
        var result = await handler.HandleAsync(command);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(
        [FromServices] RefreshTokensHandler handler,
        [FromBody] RefreshTokensRequest request)
    {
        var command = request.ToCommand();
        var result = await handler.HandleAsync(command);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}