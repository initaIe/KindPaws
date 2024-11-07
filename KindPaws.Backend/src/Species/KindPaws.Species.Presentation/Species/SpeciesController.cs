using KindPaws.Framework;
using KindPaws.Species.Application.Features.Breeds.Commands.Add;
using KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;
using KindPaws.Species.Application.Features.Breeds.Commands.SoftDelete;
using KindPaws.Species.Application.Features.Species.Commands.Create;
using KindPaws.Species.Application.Features.Species.Commands.HardDelete;
using KindPaws.Species.Application.Features.Species.Commands.SoftDelete;
using KindPaws.Species.Application.Features.Species.Queries.GetSpecies;
using KindPaws.Species.Presentation.Breeds.Requests;
using KindPaws.Species.Presentation.Species.Requests;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Species.Presentation.Species;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateSpecieRequest request,
        [FromServices] CreateSpecieHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/soft")]
    public async Task<IActionResult> SoftDelete(
        [FromRoute] Guid id,
        [FromServices] SoftDeleteSpecieHandler handler,
        CancellationToken token = default)
    {
        var command = new SoftDeleteSpecieCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> HardDelete(
        [FromRoute] Guid id,
        [FromServices] HardDeleteSpecieHandler handler,
        CancellationToken token = default)
    {
        var command = new HardDeleteSpecieCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/breeds")]
    public async Task<IActionResult> AddBreed(
        [FromRoute] Guid id,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler handler,
        CancellationToken token = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/breeds/{breedId:guid}/soft")]
    public async Task<IActionResult> SoftDeleteBreed(
        [FromRoute] Guid id,
        [FromRoute] Guid breedId,
        [FromServices] SoftDeleteBreedHandler handler,
        CancellationToken token = default)
    {
        var command = new SoftDeleteBreedCommand(id, breedId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}/breeds/{breedId:guid}/hard")]
    public async Task<IActionResult> HardDeleteBreed(
        [FromRoute] Guid id,
        [FromRoute] Guid breedId,
        [FromServices] HardDeleteBreedHandler handler,
        CancellationToken token = default)
    {
        var command = new HardDeleteBreedCommand(id, breedId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecies(
        [FromQuery] GetSpeciesRequest request,
        [FromServices] GetSpeciesHandler handler,
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
    
    [HttpGet("dapper")]
    public async Task<IActionResult> GetSpeciesDapper(
        [FromQuery] GetSpeciesRequest request,
        [FromServices] GetSpeciesDapperHandler handler,
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
}