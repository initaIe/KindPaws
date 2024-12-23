using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.OutBox;
using KindPaws.Core.OutBox.Abstractions;
using KindPaws.Core.OutBox.Database;
using KindPaws.Core.OutBox.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

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
            .AddScoped<AuthWriteWriteDbContext>()
            .AddScoped<IOutBoxWriteDbContext, AuthWriteWriteDbContext>()
            .AddScoped<IAuthReadDbContext, AuthReadDbContext>();
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