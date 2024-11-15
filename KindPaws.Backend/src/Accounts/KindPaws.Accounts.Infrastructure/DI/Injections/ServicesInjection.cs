using KindPaws.Accounts.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<AccountsSeederService>();
    }
}