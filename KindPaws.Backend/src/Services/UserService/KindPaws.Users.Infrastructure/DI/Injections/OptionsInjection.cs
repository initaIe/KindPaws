using KindPaws.Core.Options;
using KindPaws.Users.Infrastructure.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(PostgresOptions.SectionName));
        services.Configure<RabbitmqOptions>(configuration.GetRequiredSection(RabbitmqOptions.SectionName));

        return services;
    }
}