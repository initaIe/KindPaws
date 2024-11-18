using KindPaws.Core.Abstractions;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class RepositoriesInjection
{
    /// <summary>
    /// Добавление Repositories.
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(Species.Application.DI.DependencyInjection).Assembly,
            typeof(Volunteers.Application.DI.DependencyInjection).Assembly,
            typeof(Accounts.Application.DI.DependencyInjection).Assembly,
        };
        
        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}