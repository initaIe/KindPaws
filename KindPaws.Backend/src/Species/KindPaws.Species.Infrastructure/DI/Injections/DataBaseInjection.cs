using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Enums;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Infrastructure.DbContexts;
using KindPaws.Species.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Infrastructure.DI.Injections;

public static class DataBaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services
            .AddDbContexts()
            .AddUnitOfWork()
            .AddSqlConnectionFactory()
            .AddRepositories();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        return services
            .AddScoped<SpeciesWriteDbContext>()
            .AddScoped<ISpeciesReadDbContext, SpeciesReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Species);
    }

    private static IServiceCollection AddSqlConnectionFactory(this IServiceCollection services)
    {
        return services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesRepository, SpeciesRepository>();
    }
}