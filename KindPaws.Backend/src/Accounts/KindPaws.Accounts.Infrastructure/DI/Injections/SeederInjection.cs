using KindPaws.Accounts.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class SeederInjection
{
    public static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        return services.AddSingleton<AccountsSeeder>();
    }
}