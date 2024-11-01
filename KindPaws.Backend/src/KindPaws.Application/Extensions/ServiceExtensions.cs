using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Application.Validation.ExistValidators;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddValidators();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(ServiceExtensions).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(typeof(ServiceExtensions).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        services.Scan(scan => scan.FromAssemblies(typeof(ServiceExtensions).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IEntitiesExistenceValidator<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services
            .AddScoped<IBreedExistenceValidator, BreedExistenceValidator>()
            .AddScoped<ISpecieExistenceValidator, SpecieExistenceValidator>()
            .AddScoped<IPetExistenceValidator, PetExistenceValidator>()
            .AddScoped<IVolunteerExistenceValidator, VolunteerExistenceValidator>();

        return services;
    }
}