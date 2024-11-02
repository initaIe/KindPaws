using KindPaws.API.Controllers.Volunteers.Queries;
using KindPaws.API.Controllers.Volunteers.Requests;
using KindPaws.API.Extensions;
using KindPaws.API.Processors;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdatePosition;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteerById;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteers;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Volunteers;

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

        return Ok(result.Value);
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

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> AddPetPhotos(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromForm] AddPetPhotosRequest request,
        [FromServices] AddPetPhotosHandler handler,
        CancellationToken token = default)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(request.Photos);

        var command = new AddPetPhotosCommand(
            id,
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
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVolunteerById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken token = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/pets/{petId:guid}/position")]
    public async Task<IActionResult> UpdatePetPosition(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetPositionRequest request,
        [FromServices] UpdatePetPositionHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/pets/{petId:guid}/main-photo")]
    public async Task<IActionResult> SetPetMainPhoto(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromBody] SetPetMainPhotoRequest request,
        [FromServices] SetPetMainPhotoHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/soft")]
    public async Task<IActionResult> SoftDelete(
        [FromRoute] Guid id,
        [FromServices] SoftDeleteVolunteerHandler handler,
        CancellationToken token = default)
    {
        var command = new SoftDeleteVolunteerCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> HardDelete(
        [FromRoute] Guid id,
        [FromServices] HardDeleteVolunteerHandler handler,
        CancellationToken token = default)
    {
        var command = new HardDeleteVolunteerCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/pets/{petId:guid}/soft")]
    public async Task<IActionResult> SoftDeletePet(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken token = default)
    {
        var command = new SoftDeletePetCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/pets/{petId:guid}/hard")]
    public async Task<IActionResult> HardDeletePet(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromServices] HardDeletePetHandler handler,
        CancellationToken token = default)
    {
        var command = new HardDeletePetCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> DeletePetPhotos(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromBody] DeletePetPhotosRequest request,
        [FromServices] DeletePetPhotosHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}