using KindPaws.Framework;
using KindPaws.Volunteers.Application.Features.Pets.Commands.Add;
using KindPaws.Volunteers.Application.Features.Pets.Commands.AddPhotos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.HardDelete;
using KindPaws.Volunteers.Application.Features.Pets.Commands.SetMainPhoto;
using KindPaws.Volunteers.Application.Features.Pets.Commands.SoftDelete;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDelete;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDelete;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateAdditionalInfo;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateMainInfo;
using KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteerById;
using KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;
using KindPaws.Volunteers.Presentation.Volunteers.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Volunteers.Presentation.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{volunteerId:guid}/pets")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid volunteerId,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> AddPetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] AddPetPhotosRequest request,
        [FromServices] AddPetPhotosHandler handler,
        CancellationToken token)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(request.Photos);

        var command = new AddPetPhotosCommand(
            volunteerId,
            petId,
            fileDtos);

        var result = await handler.HandleAsync(command, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetVolunteers(
        [FromQuery] GetVolunteersRequest request,
        [FromServices] GetVolunteersHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("{volunteerId:guid}")]
    public async Task<IActionResult> GetVolunteerById(
        [FromRoute] Guid volunteerId,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken token)
    {
        var query = new GetVolunteerByIdQuery(volunteerId);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateVolunteerMainInfoRequest request,
        [FromServices] UpdateVolunteerMainInfoHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/additional-info")]
    public async Task<IActionResult> UpdateAdditionalInfo(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateVolunteerAdditionalInfoRequest request,
        [FromServices] UpdateVolunteerAdditionalInfoHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/main-info")]
    public async Task<IActionResult> UpdatePetMainInfo(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetMainInfoRequest request,
        [FromServices] UpdatePetMainInfoHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/additional-info")]
    public async Task<IActionResult> UpdatePetAdditionalInfo(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetAdditionalInfoRequest request,
        [FromServices] UpdatePetAdditionalInfoHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/position")]
    public async Task<IActionResult> UpdatePetPosition(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetPositionRequest request,
        [FromServices] UpdatePetPositionHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/main-photo")]
    public async Task<IActionResult> SetPetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] SetPetMainPhotoRequest request,
        [FromServices] SetPetMainPhotoHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{volunteerId:guid}/soft")]
    public async Task<IActionResult> SoftDelete(
        [FromRoute] Guid volunteerId,
        [FromServices] SoftDeleteVolunteerHandler handler,
        CancellationToken token)
    {
        var command = new SoftDeleteVolunteerCommand(volunteerId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{volunteerId:guid}/hard")]
    public async Task<IActionResult> HardDelete(
        [FromRoute] Guid volunteerId,
        [FromServices] HardDeleteVolunteerHandler handler,
        CancellationToken token)
    {
        var command = new HardDeleteVolunteerCommand(volunteerId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/soft")]
    public async Task<IActionResult> SoftDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken token)
    {
        var command = new SoftDeletePetCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/hard")]
    public async Task<IActionResult> HardDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] HardDeletePetHandler handler,
        CancellationToken token)
    {
        var command = new HardDeletePetCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> DeletePetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] DeletePetPhotosRequest request,
        [FromServices] DeletePetPhotosHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}