using KindPaws.Roles.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<RolesSeederOptions>(configuration
            .GetRequiredSection(RolesSeederOptions.SectionName));
    }
}