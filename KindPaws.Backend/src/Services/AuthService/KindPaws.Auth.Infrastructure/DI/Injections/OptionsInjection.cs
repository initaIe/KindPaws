using KindPaws.Auth.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(PostgresOptions.SectionName));
        services.Configure<AuthModuleOptions>(configuration.GetRequiredSection(AuthModuleOptions.SectionName));

        return services;
    }
}