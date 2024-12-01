using KindPaws.Framework;
using KindPaws.Framework.Authorization;
using KindPaws.Volunteers.Application.Features.Pets.Queries.GetPetById;
using KindPaws.Volunteers.Application.Features.Pets.Queries.GetPets;
using KindPaws.Volunteers.Contracts.Requests;
using KindPaws.Volunteers.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Volunteers.Presentation.Controllers;

public class PetsController : ApplicationController
{
    [Permission(Permissions.Pets.GetPet)]
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

    [Permission(Permissions.Pets.GetPet)]
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
}