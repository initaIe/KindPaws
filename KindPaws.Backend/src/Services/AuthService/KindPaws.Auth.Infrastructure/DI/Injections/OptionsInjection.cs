using KindPaws.Auth.Infrastructure.Common.Options;
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
        services.Configure<JwtBearerAuthOptions>(configuration.GetRequiredSection(JwtBearerAuthOptions.SectionName));
        services.Configure<RabbitmqOptions>(configuration.GetRequiredSection(RabbitmqOptions.SectionName));

        return services;
    }
}