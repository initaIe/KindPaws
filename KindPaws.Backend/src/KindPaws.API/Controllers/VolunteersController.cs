using FluentValidation;
using KindPaws.API.Contracts;
using KindPaws.API.Extensions;
using KindPaws.API.Response;
using KindPaws.Application.Volunteers.PetHandlers.Add;
using KindPaws.Application.Volunteers.PetHandlers.Add.DTOs;
using KindPaws.Application.Volunteers.PetHandlers.UpdateMainInfo;
using KindPaws.Application.Volunteers.VolunteerHandlers.Create;
using KindPaws.Application.Volunteers.VolunteerHandlers.Create.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.Delete;
using KindPaws.Application.Volunteers.VolunteerHandlers.Delete.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.GetById;
using KindPaws.Application.Volunteers.VolunteerHandlers.GetById.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo.DTOs;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        [FromServices] IValidator<GetVolunteerByIdRequest> validator,
        CancellationToken token = default)
    {
        var request = new GetVolunteerByIdRequest(id);

        var validationResult = await validator.ValidateAsync(request, token);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var getResult = await handler.HandleAsync(request, token);
        if (getResult.IsFailure)
            return getResult.Error.ToResponse();

        var envelope = Envelope.Ok(getResult.Value);

        return Ok(envelope);
    }

    [HttpPost("{id:guid}/pets")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken token = default)
    {
        var command = new AddPetCommand(
            id,
            request.SpecieId,
            request.BreedId,
            request.Name);

        var addPetResult = await handler.HandleAsync(command, token);
        if (addPetResult.IsFailure)
            return addPetResult.Error.ToResponse();
        
        var envelope = Envelope.Ok(addPetResult.Value);

        return Ok(envelope);
    }
    
    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/main-info")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] AddPetRequest request,
        [FromServices] UpdatePetMainInfoHandler handler,
        CancellationToken token = default)
    {
        var command = new UpdatePetMainInfoCommand(
            volunteerId,
            petId,
            request.SpecieId,
            request.BreedId,
            request.Name);

        var updatePetMainInfoResult = await handler.HandleAsync(command, token);
        if (updatePetMainInfoResult.IsFailure)
            return updatePetMainInfoResult.Error.ToResponse();
        
        var envelope = Envelope.Ok(updatePetMainInfoResult.Value);

        return Ok(envelope);
    }
}