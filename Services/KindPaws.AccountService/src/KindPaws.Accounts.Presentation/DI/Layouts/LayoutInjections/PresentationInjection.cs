using KindPaws.Accounts.Contracts;
using KindPaws.Accounts.Presentation.Contract;

namespace KindPaws.Accounts.Presentation.DI.Layouts.LayoutInjections;

public static class PresentationInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IAccountsContract, AccountsContract>();
    }
}