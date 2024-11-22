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

        services.AddSpeciesModule();
        services.AddRolesModule();
        services.AddPermissionsModule();
        services.AddVolunteersModule(configuration);
        services.AddAccountsModule(configuration);

        services.AddSerilogLogger(configuration);
        services.AddOptions(configuration);

        return services;
    }
}