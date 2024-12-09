using KindPaws.Core.Abstractions.DataBase;

namespace KindPaws.Auth.Presentation.DI.LayersInjections.Injections;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(Infrastructure.DI.DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}