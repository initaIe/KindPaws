using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Species.Infrastructure;
using KindPaws.Species.Presentation;
using KindPaws.Volunteers.Infrastructure;
using KindPaws.Volunteers.Presentation;
using Serilog;
using Serilog.Events;

namespace KindPaws.WEB;

public static class DependencyInjection
{
    // Добавление модуля Species (Infrastructure and Presentation layers) + Dapper flag.
    public static IServiceCollection AddSpeciesModule(
        this IServiceCollection services)
    {
        services.AddSpeciesInfrastructure();
        services.AddSpeciesPresentation();

        return services;
    }

    // Добавление модуля Volunteers (Infrastructure and Presentation layers).
    public static IServiceCollection AddVolunteersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddVolunteersInfrastructure(configuration);
        services.AddVolunteersPresentation();

        return services;
    }

    // Добавление логгирования и Serilog.
    public static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Seq(configuration.GetConnectionString("Seq")
                         ?? throw new NullReferenceException("Seq connection string not found"))
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();

        return services;
    }

    // Добавление QueryHandlers, CommandHandlers, ExistenceValidators and CommandValidators(FluentValidation).
    public static IServiceCollection AddApplicationLayers(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(Species.Application.DependencyInjection).Assembly,
            typeof(Volunteers.Application.DependencyInjection).Assembly,
        };

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
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
        
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}