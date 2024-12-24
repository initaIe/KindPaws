using KindPaws.Core.Options;

namespace KindPaws.Users.Presentation.DI.OthersInjections.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSeqOptions(configuration);

        return services;
    }

    private static IServiceCollection AddSeqOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SeqOptions>(configuration.GetRequiredSection(SeqOptions.SectionName));

        return services;
    }
}