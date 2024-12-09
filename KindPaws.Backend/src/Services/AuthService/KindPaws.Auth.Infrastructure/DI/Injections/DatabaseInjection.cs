using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Auth.Infrastructure.OutBox;
using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

public static class DatabaseInjection
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
            .AddScoped<AuthWriteDbContext>()
            .AddScoped<IAuthReadDbContext, AuthReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    private static IServiceCollection AddOutBox(this IServiceCollection services)
    {
        return services.AddScoped<IOutBoxRepository, OutBoxRepository>();
    }
}