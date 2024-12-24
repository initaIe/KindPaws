using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.OutBox.Abstractions;
using KindPaws.Core.OutBox.Database;
using KindPaws.Core.OutBox.Interceptors;
using KindPaws.Pets.Application.Abstractions;
using KindPaws.Pets.Infrastructure.Persistence;
using KindPaws.Pets.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Pets.Infrastructure.DI.Injections;

public static class DatabaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services
            .AddDbContexts()
            .AddUnitOfWork()
            .AddInterceptors()
            .AddRepositories();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        return services
            .AddScoped<PetsWriteDbContext>()
            .AddScoped<IOutBoxWriteDbContext, PetsWriteDbContext>()
            .AddScoped<IPetsReadDbContext, PetsReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        return services.AddSingleton<ISaveChangesInterceptor, DomainEventsToOutboxInterceptor>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IOutBoxRepository, OutBoxRepository>();
    }
}