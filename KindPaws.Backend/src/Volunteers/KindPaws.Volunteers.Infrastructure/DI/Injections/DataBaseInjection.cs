using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Enums;
using KindPaws.Volunteers.Application.Interfaces;
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
            .AddRepositories()
            .AddUnitOfWork();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        return services
            .AddScoped<VolunteersWriteDbContext>()
            .AddScoped<IVolunteersReadDbContext, VolunteersReadDbContext>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IVolunteersRepository, VolunteersRepository>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Volunteers);
    }
}