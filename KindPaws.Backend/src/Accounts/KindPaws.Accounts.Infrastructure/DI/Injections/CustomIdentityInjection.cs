using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class CustomIdentityInjection
{
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AccountsWriteDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}