using System.Reflection;
using KindPaws.Accounts.Infrastructure.DI;
using KindPaws.Core.Abstractions.DataBase;

namespace KindPaws.Accounts.Presentation.DI.Layouts.LayoutInjections;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAccountsInfrastructure(configuration);

        var assembly = typeof(Infrastructure.DI.DependencyInjection).Assembly;
        services.AddRepositories(assembly);

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}