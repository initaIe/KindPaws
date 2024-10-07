using KindPaws.Application.Volunteers;
using KindPaws.Infrastructure.Interceptors;
using KindPaws.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        return services;
    }
}