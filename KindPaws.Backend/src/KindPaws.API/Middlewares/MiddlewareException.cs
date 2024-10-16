using KindPaws.API.Response;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.API.Middlewares;

public class MiddlewareException
{
    private readonly ILogger<MiddlewareException> _logger;
    private readonly RequestDelegate _next;

    public MiddlewareException(
        RequestDelegate next,
        ILogger<MiddlewareException> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            var error = Error.Failure("server.internal", e.Message);

            var envelope = Envelope.Error(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}