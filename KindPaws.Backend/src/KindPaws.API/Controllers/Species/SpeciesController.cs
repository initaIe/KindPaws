using KindPaws.API.Controllers.Species.Requests;
using KindPaws.API.Extensions;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Delete;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Species;

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

    [HttpPost("{id:guid}/breeds")]
    public async Task<IActionResult> AddPet(
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
    
    [HttpPost("{id:guid}/breeds/{breedId:guid}")]
    public async Task<IActionResult> DeleteBreedById(
        [FromRoute] Guid id,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedHandler handler,
        CancellationToken token = default)
    {
        var command = new DeleteBreedCommand(id, breedId);

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}