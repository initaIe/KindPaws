using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections.Seeding;

public static class SeedingInjection
{
    public static IServiceCollection AddSeeding(this IServiceCollection services)
    {
        return services
            .AddAccountsSeederHostedService()
            .AddAccountsSeederService();
    }
}