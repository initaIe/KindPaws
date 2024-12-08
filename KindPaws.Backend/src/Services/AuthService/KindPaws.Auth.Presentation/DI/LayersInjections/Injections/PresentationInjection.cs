using KindPaws.Auth.Contracts;
using KindPaws.Auth.Presentation.Contract;

namespace KindPaws.Auth.Presentation.DI.LayersInjections.Injections;

public static class PresentationInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<IAuthContract, AuthContract>();

        return services;
    }
}