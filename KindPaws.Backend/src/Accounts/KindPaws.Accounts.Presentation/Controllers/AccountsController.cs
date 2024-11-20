using KindPaws.Accounts.Application.Commands.Login;
using KindPaws.Accounts.Application.Commands.RefreshTokens;
using KindPaws.Accounts.Application.Commands.Register;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Accounts.Presentation.Mappers;
using KindPaws.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [HttpPost("register")]
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

    [HttpPost("login")]
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