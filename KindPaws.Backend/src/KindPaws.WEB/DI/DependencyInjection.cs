using KindPaws.WEB.DI.Injections.Modules;
using KindPaws.WEB.DI.Injections.Others;

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
        DapperConfiguration.Configure();

        services.AddRepositories();
        services.AddApplications();

        services.AddSpeciesModule(configuration);
        services.AddRolesModule(configuration);
        services.AddPermissionsModule(configuration);
        services.AddVolunteersModule(configuration);
        services.AddAccountsModule(configuration);
        services.AddAuthModule(configuration);

        services.AddSerilogLogger(configuration);
        services.AddOptions(configuration);

        return services;
    }
}