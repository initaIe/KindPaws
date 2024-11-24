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
            .Configure<RefreshSessionOptions>(configuration.GetRequiredSection(RefreshSessionOptions.SectionName))
            .Configure<AccountsSeederOptions>(configuration.GetRequiredSection(AccountsSeederOptions.SectionName));
    }
}