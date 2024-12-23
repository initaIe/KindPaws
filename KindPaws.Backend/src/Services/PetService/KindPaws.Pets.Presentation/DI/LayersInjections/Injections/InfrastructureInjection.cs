namespace KindPaws.Pets.Presentation.DI.LayersInjections.Injections;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // var assembly = typeof(Infrastructure.DI.DependencyInjection).Assembly;
        //
        // services.Scan(scan => scan.FromAssemblies(assembly)
        //     .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
        //     .AsSelfWithInterfaces()
        //     .WithScopedLifetime());
        //
        // services.AddInfrastructureLayer(configuration);

        return services;
    }
}