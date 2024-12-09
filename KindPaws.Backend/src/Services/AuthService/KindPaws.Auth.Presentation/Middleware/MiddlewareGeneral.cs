using KindPaws.Framework.Middlewares;
using Serilog;

namespace KindPaws.Auth.Presentation.Middleware;

public static class MiddlewareGeneral
{
    public static WebApplication UseAuthMiddlewares(this WebApplication app)
    {
        // Exception middleware
        app.UseMiddlewareException();
        // Serilog middleware for logging
        app.UseSerilogRequestLogging();

        // General
        app.UseHttpsRedirection();
        // app.UseAuthentication();
        // app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}