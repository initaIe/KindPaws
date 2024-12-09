using KindPaws.Auth.Presentation.DI.WebInjections.Injections;

namespace KindPaws.Auth.Presentation.DI.WebInjections;

public static class WebInjection
{
    public static IServiceCollection AddWeb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSerilogLogger(configuration);

        return services;
    }
}