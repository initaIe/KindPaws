using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.Auth;

public static class AuthInjection
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddCustomAuthentication(configuration)
            .AddCustomAuthorization()
            .AddAuthServices()
            .AddCustomAuthProviders()
            .AddCustomAuthHandlers();

        return services;
    }
}