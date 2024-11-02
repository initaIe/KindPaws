using KindPaws.API.Extensions;
using KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPetById;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers.Volunteers;

public class PetsController : ApplicationController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVolunteerById(
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