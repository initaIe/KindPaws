using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Role;
using KindPaws.Accounts.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections.Auth;

public static class CustomIdentityInjection
{
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<Account, Role>(options => { options.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AccountsWriteDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}