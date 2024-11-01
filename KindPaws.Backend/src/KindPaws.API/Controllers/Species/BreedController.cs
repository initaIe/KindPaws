using KindPaws.API.Controllers.Species.Queries;
using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;
using KindPaws.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Species;

public class BreedController : ApplicationController
{
    [HttpGet]
    public async Task<IActionResult> GetBreedsBySpecieId(
        [FromQuery] GetBreedsWithPaginationAndFilterRequest request,
        [FromServices] GetBreedsWithPaginationAndFilterHandler handler,
        CancellationToken token = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
}