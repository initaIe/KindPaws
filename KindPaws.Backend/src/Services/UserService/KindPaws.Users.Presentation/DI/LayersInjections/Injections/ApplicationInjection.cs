using System.Reflection;
using FluentValidation;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Users.Application.DI;

namespace KindPaws.Users.Presentation.DI.LayersInjections.Injections;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Application.DI.DependencyInjection).Assembly;

        services.AddCommandHandlers(assembly);
        services.AddQueryHandlers(assembly);
        services.AddFluentValidationValidators(assembly);
        services.AddApplicationLayer();

        return services;
    }

    private static IServiceCollection AddCommandHandlers(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddQueryHandlers(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddFluentValidationValidators(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}