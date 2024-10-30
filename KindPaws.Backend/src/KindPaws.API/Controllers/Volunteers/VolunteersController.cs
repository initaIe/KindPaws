using KindPaws.API.Controllers.Volunteers.Queries;
using KindPaws.API.Controllers.Volunteers.Requests;
using KindPaws.API.Extensions;
using KindPaws.API.Processors;
using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteerById;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;
using KindPaws.Application.Models;
using KindPaws.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] ICommandHandler<Guid, CreateVolunteerCommand> handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerMainInfoRequest request,
        [FromServices] ICommandHandler<Guid, UpdateVolunteerMainInfoCommand> handler,
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
        [FromServices] ICommandHandler<Guid, UpdateVolunteerAdditionalInfoCommand> handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] ICommandHandler<Guid, DeleteVolunteerCommand> handler,
        CancellationToken token = default)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/pets")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] ICommandHandler<Guid, AddPetCommand> handler,
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
        [FromServices] ICommandHandler<Guid, UpdatePetMainInfoCommand> handler,
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
        [FromServices] ICommandHandler<Guid, UpdatePetAdditionalInfoCommand> handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id, petId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> AddPetPhotos(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromForm] AddPetPhotosRequest request,
        [FromServices] ICommandHandler<Guid, AddPetPhotosCommand> handler,
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
    public async Task<IActionResult> GetVolunteersWithPagination(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] IQueryHandler<PagedList<VolunteerDTO>, GetVolunteersWithPaginationQuery> handler,
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVolunteerById(
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<Result<VolunteerDTO, ErrorList>, GetVolunteerByIdQuery> handler,
        CancellationToken token = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}