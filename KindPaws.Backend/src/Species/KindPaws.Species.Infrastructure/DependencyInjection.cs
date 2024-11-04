using KindPaws.Core.Abstractions;
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
            .AddUnitOfWork()
            .AddRepositories();


        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<SpeciesWriteDbContext>();
        services.AddScoped<ISpeciesReadDbContext, SpeciesReadDbContext>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }
}