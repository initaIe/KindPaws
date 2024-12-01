using KindPaws.Accounts.Presentation.DI.Layouts;
using KindPaws.Accounts.Presentation.DI.Others;
using KindPaws.Accounts.Presentation.DI.Web;

namespace KindPaws.Accounts.Presentation.DI;

public static class DependencyInjection
{
    /// <summary>
    /// Добавляет все зависимости в DI.
    /// </summary>
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLayoutInjections(configuration);
        services.AddOtherInjections(configuration);
        services.AddWevInjections(configuration);

        return services;
    }
}