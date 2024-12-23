using KindPaws.Pets.Application.DI.Injections;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Pets.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediator();

        return services;
    }
}