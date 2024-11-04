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
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPetById(
        [FromRoute] Guid id,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken token = default)
    {
        var query = new GetPetByIdQuery(id);

        var result = await handler.HandleAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}