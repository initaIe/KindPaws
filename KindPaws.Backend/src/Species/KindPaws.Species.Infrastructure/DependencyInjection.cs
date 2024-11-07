using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Enums;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Infrastructure.DbContexts;
using KindPaws.Species.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services)
    {
        services
            .AddDbContexts()
            .AddDataBase()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<SpeciesWriteDbContext>();
        services.AddScoped<ISpeciesReadDbContext, SpeciesReadDbContext>();

        return services;
    }

    private static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Species);
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }
}