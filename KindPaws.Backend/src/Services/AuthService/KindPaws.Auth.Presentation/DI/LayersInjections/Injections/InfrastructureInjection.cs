using System.Reflection;
using KindPaws.Auth.Infrastructure.DI;
using KindPaws.Core.Abstractions.Database;

namespace KindPaws.Auth.Presentation.DI.LayersInjections.Injections;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(Infrastructure.DI.DependencyInjection).Assembly;

        services.AddRepositories(assembly);
        services.AddInfrastructureLayer(configuration);

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