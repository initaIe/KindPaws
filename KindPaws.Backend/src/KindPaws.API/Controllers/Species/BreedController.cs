using KindPaws.API.Controllers.Species.Queries;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Species;

public class BreedController : ApplicationController
{
    [HttpGet]
    public async Task<IActionResult> GetBreeds(
        [FromQuery] GetBreedsRequest request,
        [FromServices] GetBreedsHandler handler,
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
}