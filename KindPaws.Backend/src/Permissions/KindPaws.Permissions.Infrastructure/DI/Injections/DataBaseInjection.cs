using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI.Injections;

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
            .AddScoped<PermissionsWriteDbContext>()
            .AddScoped<IPermissionsReadDbContext, PermissionsReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Permissions);
    }
}