using KindPaws.API.Middlewares;

namespace KindPaws.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMiddlewareException(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MiddlewareException>();
    }
}