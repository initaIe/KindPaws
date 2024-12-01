namespace KindPaws.WEB.DI.Injections.Web;

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
        services.AddCustomSwaggerGen();

        return services;
    }
}