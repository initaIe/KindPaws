using KindPaws.Framework;
using KindPaws.Species.Application.Features.Breeds.Commands.Add;
using KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;
using KindPaws.Species.Application.Features.Breeds.Commands.SoftDelete;
using KindPaws.Species.Application.Features.Species.Commands.Create;
using KindPaws.Species.Application.Features.Species.Commands.HardDelete;
using KindPaws.Species.Application.Features.Species.Commands.SoftDelete;
using KindPaws.Species.Application.Features.Species.Queries.GetSpecies;
using KindPaws.Species.Contracts.Requests;
using KindPaws.Species.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Species.Presentation.Controllers;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateSpecieRequest request,
        [FromServices] CreateSpecieHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{specieId:guid}/soft")]
    public async Task<IActionResult> SoftDelete(
        [FromRoute] Guid specieId,
        [FromServices] SoftDeleteSpecieHandler handler,
        CancellationToken token)
    {
        var command = new SoftDeleteSpecieCommand(specieId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{specieId:guid}/hard")]
    public async Task<IActionResult> HardDelete(
        [FromRoute] Guid specieId,
        [FromServices] HardDeleteSpecieHandler handler,
        CancellationToken token)
    {
        var command = new HardDeleteSpecieCommand(specieId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{specieId:guid}/breeds")]
    public async Task<IActionResult> AddBreed(
        [FromRoute] Guid specieId,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand(specieId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{specieId:guid}/breeds/{breedId:guid}/soft")]
    public async Task<IActionResult> SoftDeleteBreed(
        [FromRoute] Guid specieId,
        [FromRoute] Guid breedId,
        [FromServices] SoftDeleteBreedHandler handler,
        CancellationToken token)
    {
        var command = new SoftDeleteBreedCommand(specieId, breedId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{specieId:guid}/breeds/{breedId:guid}/hard")]
    public async Task<IActionResult> HardDeleteBreed(
        [FromRoute] Guid specieId,
        [FromRoute] Guid breedId,
        [FromServices] HardDeleteBreedHandler handler,
        CancellationToken token)
    {
        var command = new HardDeleteBreedCommand(specieId, breedId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecies(
        [FromQuery] GetSpeciesRequest request,
        [FromServices] GetSpeciesHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("dapper")]
    public async Task<IActionResult> GetSpeciesDapper(
        [FromQuery] GetSpeciesRequest request,
        [FromServices] GetSpeciesDapperHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
}