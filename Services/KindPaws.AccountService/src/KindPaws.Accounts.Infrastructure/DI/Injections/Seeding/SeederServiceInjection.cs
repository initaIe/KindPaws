using KindPaws.Accounts.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections.Seeding;

public static class SeederServiceInjection
{
    public static IServiceCollection AddAccountsSeederService(this IServiceCollection services)
    {
        return services.AddScoped<AccountsSeederService>();
    }
}