using KindPaws.Auth.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<JwtBearerOptions>(
            configuration.GetRequiredSection(JwtBearerOptions.SectionName));
    }
}