using Serilog;

namespace KindPaws.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddLoggers();

        return services;
    }

    private static IServiceCollection AddLoggers(this IServiceCollection services)
    {
        services.AddSerilog();

        return services;
    }
}