using KindPaws.API.Extensions;
using KindPaws.Application.Volunteers.CreateVolunteer;
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
        var result = await handler.HandleAsync(request, token);

        return result.ToResponse();
    }
}