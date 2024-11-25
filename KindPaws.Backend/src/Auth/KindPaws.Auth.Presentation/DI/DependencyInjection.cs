using KindPaws.Auth.Contracts;
using KindPaws.Auth.Presentation.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IAuthContract, AuthContract>();
    }
}