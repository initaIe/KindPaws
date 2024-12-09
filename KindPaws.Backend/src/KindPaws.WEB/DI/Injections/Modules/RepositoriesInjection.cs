using KindPaws.Core.Abstractions.Database;

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
            typeof(Species.Infrastructure.DI.DependencyInjection).Assembly,
            typeof(Volunteers.Infrastructure.DI.DependencyInjection).Assembly,
            typeof(Accounts.Infrastructure.DI.DependencyInjection).Assembly,
            typeof(Roles.Infrastructure.DI.DependencyInjection).Assembly,
            typeof(Permissions.Infrastructure.DI.DependencyInjection).Assembly,
            typeof(Auth.Infrastructure.DI.DependencyInjection).Assembly,
        };

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}