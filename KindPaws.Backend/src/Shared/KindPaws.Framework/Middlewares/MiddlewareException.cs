using KindPaws.Core.Models;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KindPaws.Framework.Middlewares;

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
        catch (Exception ex)
        {
            _logger.LogError("Exception: {ex}", ex.Message);

            var error = Error.Failure("server.internal", ex.Message);

            var envelope = Envelope.Error(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class MiddlewareExceptionExtensions
{
    public static IApplicationBuilder UseMiddlewareException(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MiddlewareException>();
    }
}