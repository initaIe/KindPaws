using FluentValidation;
using KindPaws.API.Extensions;
using KindPaws.API.Response;
using KindPaws.Application.Volunteers.Create;
using KindPaws.Application.Volunteers.Create.DTOs;
using KindPaws.Application.Volunteers.UpdateMainInfo;
using KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken token = default)
    {
        var createResult = await handler.HandleAsync(request, token);
        if (createResult.IsFailure)
            return createResult.Error.ToResponse();

        var envelope = Envelope.Ok(createResult.Value);

        return Ok(envelope);
    }

    // [HttpPatch("{id:guid}/main-info")]
    // public async Task<IActionResult> UpdateMainInfo(
    //     [FromServices] UpdateVolunteerMainInfoHandler handler,
    //     [FromServices] IValidator<UpdateVolunteerMainInfoRequest> validator,
    //     [FromRoute] Guid id,
    //     CancellationToken token = default)
    // {
    //     var request = new UpdateVolunteerMainInfoRequest(id);
    //
    //     var validationResult = await validator.ValidateAsync(request, token);
    //     if (!validationResult.IsValid) return validationResult.ToValidationErrorResponse();
    //
    //     var updateResult = await handler.HandleAsync(request, token);
    //     if (updateResult.IsFailure)
    //         return updateResult.Error.ToResponse();
    //
    //     var envelope = Envelope.Ok(updateResult.Value);
    //
    //     return Ok(envelope);
    // }
}