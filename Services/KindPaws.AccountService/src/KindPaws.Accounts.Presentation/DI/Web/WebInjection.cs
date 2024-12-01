using KindPaws.Accounts.Presentation.DI.Web.WebInjections;

namespace KindPaws.Accounts.Presentation.DI.Web;

public static class WebInjection
{
    public static IServiceCollection AddWevInjections(
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