using KindPaws.WEB.DI.Injections.Modules;
using KindPaws.WEB.DI.Injections.Others;
using KindPaws.WEB.DI.Injections.Web;

namespace KindPaws.WEB.DI;

public static class DependencyInjection
{
    /// <summary>
    /// Добавляет все зависимости в DI.
    /// </summary>
    public static IServiceCollection AddAllDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configurations
        DapperConfiguration.Configure();

        // Scrutor
        services.AddRepositories();
        services.AddApplications();

        // Modules
        services.AddSpeciesModule(configuration);
        services.AddRolesModule(configuration);
        services.AddPermissionsModule(configuration);
        services.AddVolunteersModule(configuration);
        services.AddAccountsModule(configuration);
        services.AddAuthModule(configuration);
        services.AddVolunteersModule(configuration);

        // Others
        services.AddOptions(configuration);
        
        // Web
        services.AddWeb(configuration);

        return services;
    }
}