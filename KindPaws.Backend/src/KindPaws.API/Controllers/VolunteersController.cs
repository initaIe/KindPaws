using KindPaws.API.Extensions;
using KindPaws.API.Response;
using KindPaws.Application.Volunteers.CreateVolunteer;
using KindPaws.Application.Volunteers.CreateVolunteer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken token = default)
    {
        throw new Exception();
        
        var createResult = await handler.HandleAsync(request, token);

        if (createResult.IsFailure)
            return createResult.Error.ToResponse();

        var envelope = Envelope.Ok(createResult.Value);

        return Ok(envelope);
    }
}