using KindPaws.Framework;
using KindPaws.Framework.Authorization;
using KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;
using KindPaws.Species.Contracts.Requests;
using KindPaws.Species.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Species.Presentation.Controllers;

public class BreedsController : ApplicationController
{
    [Permission(Permissions.Breeds.GetBreed)]
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
}