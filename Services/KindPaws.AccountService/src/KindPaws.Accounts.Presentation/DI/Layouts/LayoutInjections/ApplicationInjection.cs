using System.Reflection;
using FluentValidation;
using KindPaws.Core.Abstractions.Handlers;

namespace KindPaws.Accounts.Presentation.DI.Layouts.LayoutInjections;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Application.DI.DependencyInjection).Assembly;

        services.AddHandlers(assembly);
        services.AddValidators(assembly);

        return services;
    }


    private static IServiceCollection AddHandlers(
        this IServiceCollection services,
        Assembly assembly)
    {
        // CommandHandlers
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        // QueryHandlers
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddValidators(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.AddValidatorsFromAssemblies([assembly]);

        return services;
    }
}