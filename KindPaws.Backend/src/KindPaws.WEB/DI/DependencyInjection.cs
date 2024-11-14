using Dapper;
using KindPaws.WEB.DI.Injections.Authorization;
using KindPaws.WEB.DI.Injections.Modules;
using KindPaws.WEB.DI.Injections.Others;

namespace KindPaws.WEB.DI;

public static class DependencyInjection
{
    /// <summary>
    /// Добавляет все зависимости в DI.
    /// </summary>
    public static IServiceCollection AddAll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplications();

        services.AddSpeciesModule();
        services.AddAccountsModule(configuration);
        services.AddVolunteersModule(configuration);

        services.AddSerilogLogger(configuration);
        services.AddAuthorizationServices();
        services.AddOptions(configuration);

        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}