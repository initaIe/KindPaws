using KindPaws.Core.Options;

namespace KindPaws.Auth.Presentation.DI.OtherInjections.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SeqOptions>(configuration.GetRequiredSection(SeqOptions.SectionName));

        return services;
    }
}