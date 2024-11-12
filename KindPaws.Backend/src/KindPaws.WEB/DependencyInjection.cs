using Dapper;
using FluentValidation;
using KindPaws.Accounts.Infrastructure;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.Core.Abstractions;
using KindPaws.Framework.Authorization;
using KindPaws.Species.Infrastructure;
using KindPaws.Species.Presentation;
using KindPaws.Volunteers.Infrastructure;
using KindPaws.Volunteers.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
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
    
    // Добавление модуля Accounts (Infrastructure and Presentation layers).
    public static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAccountsInfrastructure(configuration);
        // services.AddAccountsPresentation();

        return services;
    }

    // Добавление QueryHandlers, CommandHandlers, ExistenceValidators and CommandValidators(FluentValidation).
    public static IServiceCollection AddApplicationLayers(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(Species.Application.DependencyInjection).Assembly,
            typeof(Volunteers.Application.DependencyInjection).Assembly,
            typeof(Accounts.Application.DependencyInjection).Assembly,
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
    
    // Добавление логгирования и Serilog.
    public static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // TODO: add SerilogOptions
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
    
    public static IServiceCollection AddAuthServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        
        return services;
    }
}