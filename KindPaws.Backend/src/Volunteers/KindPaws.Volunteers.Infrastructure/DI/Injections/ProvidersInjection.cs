using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        return services.AddScoped<IFileProvider, MinioProvider>();
    }
}