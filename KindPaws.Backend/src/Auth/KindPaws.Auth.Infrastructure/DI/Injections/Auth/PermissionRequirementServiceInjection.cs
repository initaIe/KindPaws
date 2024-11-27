using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.Auth;

public static class PermissionRequirementServiceInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        return services.AddScoped<PermissionRequirementService>();
    }
}