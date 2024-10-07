using FluentValidation;
using KindPaws.API.Extensions;
using KindPaws.API.Response;
using KindPaws.Application.Extensions;
using KindPaws.Application.Volunteers.Handlers.Create;
using KindPaws.Application.Volunteers.Handlers.Create.DTOs;
using KindPaws.Application.Volunteers.Handlers.GetById;
using KindPaws.Application.Volunteers.Handlers.GetById.DTOs;
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

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromServices] GetByIdVolunteerHandler handler,
        [FromBody] GetByIdVolunteerRequest request,
        CancellationToken token = default)
    {
        var getByIdResult = await handler.HandleAsync(request, token);
        if (getByIdResult.IsFailure)
            return getByIdResult.Error.ToResponse();

        var envelope = Envelope.Ok(getByIdResult.Value);

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
        if (validationResult.IsValid) return validationResult.ToValidationErrorResponse();

        var updateResult = await handler.HandleAsync(request, token);
        if (updateResult.IsFailure)
            return updateResult.Error.ToResponse();

        var envelope = Envelope.Ok(updateResult.Value);

        return Ok(envelope);
    }
}