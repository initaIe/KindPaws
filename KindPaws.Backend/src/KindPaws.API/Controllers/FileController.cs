using KindPaws.API.Extensions;
using KindPaws.Application.Providers;
using KindPaws.Application.Providers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers;

public class FileController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> UploadFile(
        IFormFile file,
        [FromServices] FileService handler,
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var uploadObjectData = new UploadObjectData(
            "photos",
            Guid.NewGuid().ToString(),
            stream);

        var result = await handler.UploadAsync(uploadObjectData, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.IsSuccess);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> UploadFile(
        [FromRoute] Guid id,
        [FromServices] FileService handler,
        CancellationToken cancellationToken)
    {
        var getObjectData = new GetObjectData(
            "photos",
            id.ToString());

        var result = await handler.GetLinkAsync(getObjectData, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFile(
        [FromRoute] Guid id,
        [FromServices] FileService handler,
        CancellationToken cancellationToken)
    {
        var deleteObjectData = new DeleteObjectData(
            "photos",
            id.ToString());

        var result = await handler.DeleteAsync(deleteObjectData, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.IsSuccess);
    }
}