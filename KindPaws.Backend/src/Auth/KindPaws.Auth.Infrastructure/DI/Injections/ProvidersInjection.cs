using KindPaws.Auth.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        return services.AddSingleton<ITokenProvider, TokenProvider>(); 
    }
}