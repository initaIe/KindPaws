using KindPaws.API.Contracts.Volunteers;
using KindPaws.API.Extensions;
using KindPaws.API.Response;
using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.PetHandlers.Add;
using KindPaws.Application.Volunteers.PetHandlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.PetHandlers.UpdateMainInfo;
using KindPaws.Application.Volunteers.PetHandlers.UpdatePhotos;
using KindPaws.Application.Volunteers.VolunteerHandlers.Create;
using KindPaws.Application.Volunteers.VolunteerHandlers.Delete;
using KindPaws.Application.Volunteers.VolunteerHandlers.GetById;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerMainInfoRequest request,
        [FromServices] UpdateVolunteerMainInfoHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpPut("{id:guid}/additional-info")]
    public async Task<IActionResult> UpdateAdditionalInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerAdditionalInfoRequest request,
        [FromServices] UpdateVolunteerAdditionalInfoHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken token = default)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken token = default)
    {
        var command = new GetVolunteerByIdCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpPost("{id:guid}/pets")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpPut("{id:guid}/pets/{petId:guid}/main-info")]
    public async Task<IActionResult> UpdatePetMainInfo(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetMainInfoRequest request,
        [FromServices] UpdatePetMainInfoHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpPut("{id:guid}/pets/{petId:guid}/additional-info")]
    public async Task<IActionResult> UpdatePetAdditionalInfo(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetAdditionalInfoRequest request,
        [FromServices] UpdatePetAdditionalInfoHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        var envelope = Envelope.Ok(result.Value);

        return Ok(envelope);
    }

    [HttpPut("{id:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> UpdatePetPhotos(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromForm] UpdatePetPhotosRequest request,
        [FromServices] UpdatePetPhotosHandler handler,
        CancellationToken token = default)
    {
        List<FileDTO> photoDtos = [];
        try
        {
            foreach (var photo in request.Photos)
            {
                var stream = photo.OpenReadStream();
                photoDtos.Add(new FileDTO(stream, photo.ContentType, photo.FileName));
            }

            var command = new UpdatePetPhotosCommand(id, petId, photoDtos);
            var result = await handler.HandleAsync(command, token);
            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }
        finally
        {
            foreach (var photoDto in photoDtos)
            {
                await photoDto.Stream.DisposeAsync();
            }
        }
    }
}