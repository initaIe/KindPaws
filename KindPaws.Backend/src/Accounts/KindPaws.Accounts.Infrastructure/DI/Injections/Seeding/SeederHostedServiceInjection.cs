using KindPaws.Accounts.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections.Seeding;

public static class SeederHostedServiceInjection
{
    public static IServiceCollection AddAccountsSeederHostedService(this IServiceCollection services)
    {
        return services.AddHostedService<AccountsSeederHostedService>();
    }
}