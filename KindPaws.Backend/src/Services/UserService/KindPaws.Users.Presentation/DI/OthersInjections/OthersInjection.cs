using KindPaws.Users.Presentation.DI.OthersInjections.Injections;

namespace KindPaws.Users.Presentation.DI.OthersInjections;

public static class OthersInjection
{
    public static IServiceCollection AddOthers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);

        return services;
    }
}