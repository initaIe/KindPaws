using KindPaws.Accounts.Infrastructure.DI;
using KindPaws.Accounts.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class AccountsInjection
{
    /// <summary>
    /// Добавление модуля Accounts (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAccountsInfrastructure(configuration);
        services.AddAccountsPresentation();

        return services;
    }
}