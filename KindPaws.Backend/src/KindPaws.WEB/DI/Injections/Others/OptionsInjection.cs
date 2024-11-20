using KindPaws.Core.Options;

namespace KindPaws.WEB.DI.Injections.Others;

// TODO: move to FRAMEWORK?
public static class OptionsInjection
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(PostgresOptions.SectionName));
        services.Configure<SeqOptions>(configuration.GetRequiredSection(SeqOptions.SectionName));

        return services;
    }
}