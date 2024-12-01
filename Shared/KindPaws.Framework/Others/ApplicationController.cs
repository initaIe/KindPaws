using KindPaws.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.Framework.Others;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}