using KindPaws.Accounts.Presentation.DI.Others.OtherInjections;

namespace KindPaws.Accounts.Presentation.DI.Others;

public static class OtherInjection
{
    public static IServiceCollection AddOtherInjections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);

        return services;
    }
}