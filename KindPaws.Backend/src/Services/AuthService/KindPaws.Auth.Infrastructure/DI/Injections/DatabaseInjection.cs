using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.Persistence;
using KindPaws.Auth.Infrastructure.Persistence.DbContexts;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Interceptors;
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
            .AddInterceptors();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        return services
            .AddScoped<AuthWriteDbContext>()
            .AddScoped<IOutBoxWriteDbContext, AuthWriteDbContext>()
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
}