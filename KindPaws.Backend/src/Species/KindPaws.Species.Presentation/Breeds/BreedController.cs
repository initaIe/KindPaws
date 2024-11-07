using KindPaws.Framework;
using KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;
using KindPaws.Species.Presentation.Breeds.Requests;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Species.Presentation.Breeds;

public class BreedController : ApplicationController
{
    [HttpGet]
    public async Task<IActionResult> GetBreeds(
        [FromQuery] GetBreedsRequest request,
        [FromServices] GetBreedsHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }

    [HttpGet("dapper")]
    public async Task<IActionResult> GetBreedsDapper(
        [FromQuery] GetBreedsRequest request,
        [FromServices] GetBreedsDapperHandler handler,
        CancellationToken token)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
}