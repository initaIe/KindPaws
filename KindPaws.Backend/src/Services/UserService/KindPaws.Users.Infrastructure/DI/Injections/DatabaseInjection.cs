using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Interceptors;
using KindPaws.Users.Application.Abstractions;
using KindPaws.Users.Infrastructure.Persistence;
using KindPaws.Users.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Infrastructure.DI.Injections;

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
            .AddScoped<UsersWriteDbContext>()
            .AddScoped<IOutBoxWriteDbContext, UsersWriteDbContext>()
            .AddScoped<IInBoxWriteDbContext, UsersWriteDbContext>()
            .AddScoped<IGeneralBoxDbContext, UsersWriteDbContext>()
            .AddScoped<IUsersReadDbContext, UsersReadDbContext>();
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