using System.Reflection;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Roles.Infrastructure.DI;

namespace KindPaws.Roles.Presentation.DI.Layouts.LayoutInjections;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRolesInfrastructure(configuration);

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