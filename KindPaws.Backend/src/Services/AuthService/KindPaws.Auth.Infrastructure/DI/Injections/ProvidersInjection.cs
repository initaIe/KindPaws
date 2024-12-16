using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddRefreshSessionOptionsProvider();
        services.AddPasswordHashProvider();
        services.AddTokenProvider();

        return services;
    }

    private static IServiceCollection AddPasswordHashProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IPasswordHashProvider, PasswordHashProvider>();
    }

    private static IServiceCollection AddTokenProvider(this IServiceCollection services)
    {
        return services.AddSingleton<ITokenProvider, TokenProvider>();
    }

    private static IServiceCollection AddRefreshSessionOptionsProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IRefreshSessionOptionsProvider, RefreshSessionOptionsProvider>();
    }
}