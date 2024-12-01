using KindPaws.Core.Options;

namespace KindPaws.Accounts.Presentation.DI.Others.OtherInjections;

public static class CustomOptionsInjection
{
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(PostgresOptions.SectionName));
        services.Configure<SeqOptions>(configuration.GetRequiredSection(SeqOptions.SectionName));

        return services;
    }
}