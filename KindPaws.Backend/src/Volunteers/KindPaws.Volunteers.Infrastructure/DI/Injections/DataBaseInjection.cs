using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Enums;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using KindPaws.Volunteers.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class DataBaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services
            .AddDbContexts()
            .AddSqlConnectionFactory()
            .AddLockRepositories()
            .AddUnitOfWork();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        return services
            .AddScoped<VolunteersWriteDbContext>()
            .AddScoped<IVolunteersReadDbContext, VolunteersReadDbContext>();
    }

    private static IServiceCollection AddSqlConnectionFactory(this IServiceCollection services)
    {
        return services.AddKeyedScoped<ISqlConnectionFactory, SqlConnectionFactory>(Modules.Volunteers);
    }
    
    private static IServiceCollection AddLockRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IVolunteersLockService, VolunteersLockService>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Volunteers);
    }
}