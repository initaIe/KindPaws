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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPetById(
        [FromRoute] Guid id,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken token)
    {
        var query = new GetPetByIdQuery(id);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}/dapper")]
    public async Task<IActionResult> GetPetByIdDapper(
        [FromRoute] Guid id,
        [FromServices] GetPetByIdDapperHandler handler,
        CancellationToken token)
    {
        var query = new GetPetByIdQuery(id);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}