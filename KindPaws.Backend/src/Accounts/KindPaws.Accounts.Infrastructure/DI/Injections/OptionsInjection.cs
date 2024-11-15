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
            .Configure<JwtBearerOptions>(configuration.GetRequiredSection(JwtBearerOptions.SectionName))
            .Configure<AccountsSeedingOptions>(configuration.GetRequiredSection(AccountsSeedingOptions.SectionName))
            .Configure<RefreshTokenOptions>(configuration.GetRequiredSection(RefreshTokenOptions.SectionName));
    }
}