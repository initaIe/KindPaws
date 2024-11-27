using KindPaws.Auth.Infrastructure.DI;
using KindPaws.Auth.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class AuthInjection
{
    /// <summary>
    /// Добавление модуля Auth (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddAuthModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthInfrastructure(configuration);
        services.AddAuthPresentation();

        return services;
    }
}