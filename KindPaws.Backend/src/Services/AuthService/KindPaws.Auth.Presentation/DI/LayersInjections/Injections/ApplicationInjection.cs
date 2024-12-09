using FluentValidation;
using KindPaws.Core.Abstractions.Handlers;

namespace KindPaws.Auth.Presentation.DI.LayersInjections.Injections;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Application.DI.DependencyInjection).Assembly;

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

        // FluentValidation validators
        services.AddValidatorsFromAssemblies([assembly]);

        return services;
    }
}