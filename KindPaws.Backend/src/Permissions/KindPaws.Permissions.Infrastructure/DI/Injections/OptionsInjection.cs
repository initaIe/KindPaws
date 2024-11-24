using KindPaws.Permissions.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<PermissionsSeederOptions>(configuration
            .GetRequiredSection(PermissionsSeederOptions.SectionName));
    }
}