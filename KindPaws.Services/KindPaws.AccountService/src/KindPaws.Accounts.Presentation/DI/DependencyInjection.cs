using KindPaws.Accounts.Contracts;
using KindPaws.Accounts.Presentation.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IAccountsContract, AccountsContract>();
    }
}