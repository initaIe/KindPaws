using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Infrastructure.DI.Injections;

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
            .AddScoped<RolesWriteDbContext>()
            .AddScoped<IRolesReadDbContext, RolesReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Roles);
    }
}