using KindPaws.Roles.Contracts;
using KindPaws.Roles.Presentation.Contract;

namespace KindPaws.Roles.Presentation.DI.Layouts.LayoutInjections;

public static class PresentationInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IRolesContract, RolesContract>();
    }
}