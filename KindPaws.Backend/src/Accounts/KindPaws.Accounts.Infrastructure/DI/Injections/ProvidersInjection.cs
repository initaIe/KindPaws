using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        return services.AddTransient<IPasswordHashProvider, PasswordHashProvider>();
    }
}