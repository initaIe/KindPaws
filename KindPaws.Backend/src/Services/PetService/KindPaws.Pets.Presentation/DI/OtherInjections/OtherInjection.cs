using KindPaws.Pets.Presentation.DI.OtherInjections.Injections;

namespace KindPaws.Pets.Presentation.DI.OtherInjections;

public static class OtherInjection
{
    public static IServiceCollection AddOther(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);

        return services;
    }
}