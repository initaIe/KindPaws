using KindPaws.Framework;
using KindPaws.Volunteers.Application.Features.Pets.Queries.GetPetById;
using KindPaws.Volunteers.Application.Features.Pets.Queries.GetPets;
using KindPaws.Volunteers.Presentation.Pets.Requests;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Volunteers.Presentation.Pets;

public class PetsController : ApplicationController
{
    [HttpGet]
    public async Task<IActionResult> GetPets(
        [FromQuery] GetPetsRequest request,
        [FromServices] GetPetsHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("dapper")]
    public async Task<IActionResult> GetPetsDapper(
        [FromQuery] GetPetsRequest request,
        [FromServices] GetPetsDapperHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("{petId:guid}")]
    public async Task<IActionResult> GetPetById(
        [FromRoute] Guid petId,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken token)
    {
        var query = new GetPetByIdQuery(petId);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("{petId:guid}/dapper")]
    public async Task<IActionResult> GetPetByIdDapper(
        [FromRoute] Guid petId,
        [FromServices] GetPetByIdDapperHandler handler,
        CancellationToken token)
    {
        var query = new GetPetByIdQuery(petId);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}