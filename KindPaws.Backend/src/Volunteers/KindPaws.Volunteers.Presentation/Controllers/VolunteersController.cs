using KindPaws.Framework;
using KindPaws.Framework.Authorization;
using KindPaws.Volunteers.Application.Features.Pets.Commands.AddPet;
using KindPaws.Volunteers.Application.Features.Pets.Commands.AddPetPhotos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePetPhotos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.HardDeletePet;
using KindPaws.Volunteers.Application.Features.Pets.Commands.SetPetMainPhoto;
using KindPaws.Volunteers.Application.Features.Pets.Commands.SoftDeletePet;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetAdditionalInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetMainInfo;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetPosition;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.CreateVolunteer;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDeleteVolunteer;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDeleteVolunteer;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfoVolunteer;
using KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteerById;
using KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;
using KindPaws.Volunteers.Contracts.Requests;
using KindPaws.Volunteers.Presentation.Converters;
using KindPaws.Volunteers.Presentation.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Volunteers.Presentation.Controllers;

public class VolunteersController : ApplicationController
{
    // [Permission(Permissions.Volunteers.CreateVolunteer)]
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

    // [Permission(Permissions.Volunteers.AddPet)]
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

    [Permission(Permissions.Volunteers.AddPetPhoto)]
    [HttpPost("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> AddPetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] AddPetPhotosHandler handler,
        CancellationToken token)
    {
        await using var fileProcessor = new FormFileConverter();
        var fileDtos = fileProcessor.Process(files);

        var command = new AddPetPhotosCommand(
            volunteerId,
            petId,
            fileDtos);

        var result = await handler.HandleAsync(command, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Volunteers.GetVolunteer)]
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

    [Permission(Permissions.Volunteers.GetVolunteer)]
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

    // [Permission(Permissions.Volunteers.UpdateVolunteer)]
    [HttpPut("{volunteerId:guid}/info")]
    public async Task<IActionResult> UpdateInfo(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateVolunteerInfoRequest request,
        [FromServices] UpdateVolunteerInfoHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(volunteerId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Volunteers.UpdatePet)]
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

    [Permission(Permissions.Volunteers.UpdatePet)]
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

    [Permission(Permissions.Volunteers.UpdatePetPosition)]
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

    [Permission(Permissions.Volunteers.SetPetMainPhoto)]
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

    [Permission(Permissions.Volunteers.SoftDeleteVolunteer)]
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

    [Permission(Permissions.Volunteers.HardDeleteVolunteer)]
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

    [Permission(Permissions.Volunteers.SoftDeletePet)]
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

    [Permission(Permissions.Volunteers.HardDeletePet)]
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

    [Permission(Permissions.Volunteers.DeletePetPhoto)]
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