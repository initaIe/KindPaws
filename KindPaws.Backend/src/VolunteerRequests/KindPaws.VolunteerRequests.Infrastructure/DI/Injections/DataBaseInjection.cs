using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.Enums;
using KindPaws.VolunteerRequests.Application.Abstractions;
using KindPaws.VolunteerRequests.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.VolunteerRequests.Infrastructure.DI.Injections;

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
            .AddScoped<VolunteerRequestsWriteDbContext>()
            .AddScoped<IVolunteerRequestsReadDbContext, VolunteerRequestsReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.VolunteerRequest);
    }
}