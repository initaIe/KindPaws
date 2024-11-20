using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;

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
        };

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IEntitiesExistenceValidator<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}