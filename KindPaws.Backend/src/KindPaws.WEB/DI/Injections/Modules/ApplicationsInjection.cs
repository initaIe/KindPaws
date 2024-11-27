using FluentValidation;
using KindPaws.Core.Abstractions.Handlers;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class ApplicationsInjection
{
    /// <summary>
    /// Добавление QueryHandlers, CommandHandlers, ExistenceValidators and CommandValidators(FluentValidation).
    /// </summary>
    public static IServiceCollection AddApplications(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(Species.Application.DI.DependencyInjection).Assembly,
            typeof(Volunteers.Application.DI.DependencyInjection).Assembly,
            typeof(Accounts.Application.DI.DependencyInjection).Assembly,
            typeof(Roles.Application.DI.DependencyInjection).Assembly,
            typeof(Permissions.Application.DI.DependencyInjection).Assembly,
            typeof(Auth.Application.DI.DependencyInjection).Assembly,
        };

        // CommandHandlers
        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        // QueryHandlers
        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}