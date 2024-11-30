using KindPaws.Accounts.Application.Features.Accounts.Commands.CreateAccount;
using KindPaws.Accounts.Application.Features.Accounts.Commands.DeleteAccount;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.AddRefreshSession;
using KindPaws.Accounts.Application.Features.RefreshSessions.Commands.DeleteRefreshSession;
using KindPaws.Accounts.Contracts.Requests;
using KindPaws.Accounts.Presentation.Mappers;
using KindPaws.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAccountRequest request,
        [FromServices] CreateAccountHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{accountId:guid}/refresh-sessions")]
    public async Task<IActionResult> AddRefreshSession(
        [FromRoute] Guid accountId,
        [FromBody] AddRefreshSessionRequest request,
        [FromServices] AddRefreshSessionHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(accountId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{accountId:guid}/refresh-sessions/{refreshSessionId:guid}")]
    public async Task<IActionResult> DeleteRefreshSession(
        [FromRoute] Guid accountId,
        [FromRoute] Guid refreshSessionId,
        [FromServices] DeleteRefreshSessionHandler handler,
        CancellationToken token)
    {
        var command = new DeleteRefreshSessionCommand(accountId, refreshSessionId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{accountId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid accountId,
        [FromServices] DeleteAccountHandler handler,
        CancellationToken token)
    {
        var command = new DeleteAccountCommand(accountId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}