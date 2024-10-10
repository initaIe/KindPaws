using FluentValidation;
using KindPaws.API.Extensions;
using KindPaws.API.Response;
using KindPaws.Application.Volunteers.Handlers.Create;
using KindPaws.Application.Volunteers.Handlers.Create.DTOs;
using KindPaws.Application.Volunteers.Handlers.Delete;
using KindPaws.Application.Volunteers.Handlers.Delete.DTOs;
using KindPaws.Application.Volunteers.Handlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.Handlers.UpdateAdditionalInfo.DTOs;
using KindPaws.Application.Volunteers.Handlers.UpdateMainInfo;
using KindPaws.Application.Volunteers.Handlers.UpdateMainInfo.DTOs;
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

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerMainInfoHandler handler,
        [FromServices] IValidator<UpdateVolunteerMainInfoRequest> validator,
        [FromBody] UpdateVolunteerMainInfoDTO dto,
        CancellationToken token = default)
    {
        var request = new UpdateVolunteerMainInfoRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, token);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var updateResult = await handler.HandleAsync(request, token);
        if (updateResult.IsFailure)
            return updateResult.Error.ToResponse();

        var envelope = Envelope.Ok(updateResult.Value);

        return Ok(envelope);
    }

    [HttpPut("{id:guid}/additional-info")]
    public async Task<IActionResult> UpdateAdditionalInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerAdditionalInfoHandler handler,
        [FromServices] IValidator<UpdateVolunteerAdditionalInfoRequest> validator,
        [FromBody] UpdateVolunteerAdditionalInfoDTO dto,
        CancellationToken token = default)
    {
        var request = new UpdateVolunteerAdditionalInfoRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, token);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var updateResult = await handler.HandleAsync(request, token);
        if (updateResult.IsFailure)
            return updateResult.Error.ToResponse();

        var envelope = Envelope.Ok(updateResult.Value);

        return Ok(envelope);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
        CancellationToken token = default)
    {
        var request = new DeleteVolunteerRequest(id);

        var validationResult = await validator.ValidateAsync(request, token);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var deleteResult = await handler.HandleAsync(request, token);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToResponse();

        var envelope = Envelope.Ok(deleteResult.Value);

        return Ok(envelope);
    }
}