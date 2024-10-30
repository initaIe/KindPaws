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
        [FromQuery] GetBreedsBySpecieIdWithPaginationQuery request,
        [FromServices] IQueryHandler<PagedList<BreedDTO>, GetBreedsBySpecieIdWithPaginationQuery> handler,
        CancellationToken token = default)
    {
        var query = new GetBreedsBySpecieIdWithPaginationQuery(request.SpecieId, request.Pagination);

        var result = await handler.HandleAsync(query, token);

        return Ok(result);
    }
}