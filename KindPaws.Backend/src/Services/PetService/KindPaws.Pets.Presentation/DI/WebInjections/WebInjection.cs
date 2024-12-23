using KindPaws.Pets.Presentation.DI.WebInjections.Injections;

namespace KindPaws.Pets.Presentation.DI.WebInjections;

public static class WebInjection
{
    public static IServiceCollection AddWeb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddCustomSwaggerGen();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSerilogLogger(configuration);

        return services;
    }
}