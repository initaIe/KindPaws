using KindPaws.Core.Abstractions;
using KindPaws.Core.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Core.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}