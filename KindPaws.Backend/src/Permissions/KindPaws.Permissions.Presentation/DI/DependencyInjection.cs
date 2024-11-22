using KindPaws.Permissions.Contracts;
using KindPaws.Permissions.Presentation.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddPermissionsPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IPermissionsContract, PermissionsContract>();
    }
}