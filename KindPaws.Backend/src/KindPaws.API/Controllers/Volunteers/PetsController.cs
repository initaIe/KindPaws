using KindPaws.API.Controllers.Volunteers.Queries;
using KindPaws.API.Extensions;
using KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPetById;
using KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Volunteers;

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