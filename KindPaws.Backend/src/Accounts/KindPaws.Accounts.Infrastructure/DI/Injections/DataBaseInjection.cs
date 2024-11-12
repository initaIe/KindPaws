using KindPaws.Accounts.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class DataBaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services.AddScoped<AccountsWriteDbContext>();
    }
}