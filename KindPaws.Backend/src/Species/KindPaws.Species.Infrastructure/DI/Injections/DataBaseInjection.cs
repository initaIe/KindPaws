using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.Enums;
using KindPaws.Species.Application.Abstractions;
using KindPaws.Species.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Infrastructure.DI.Injections;

public static class DataBaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services
            .AddDbContexts()
            .AddUnitOfWork();
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
}