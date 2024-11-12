using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IFilesCleanerService, FilesCleanerService>();
    }
}