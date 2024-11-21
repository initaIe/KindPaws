using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Enums;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class DataBaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services
            .AddDbContexts()
            .AddSqlConnectionFactory()
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

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Volunteers);
    }
}