using KindPaws.Accounts.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .Configure<JwtAccessTokenOptions>(configuration.GetRequiredSection(JwtAccessTokenOptions.JwtAccessToken))
            .Configure<AccountsSeederOptions>(configuration.GetRequiredSection(AccountsSeederOptions.AccountsSeeder));
    }
}