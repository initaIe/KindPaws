using KindPaws.Application.Volunteers;
using KindPaws.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Infrastructure;

public static class Inject
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
    }
}