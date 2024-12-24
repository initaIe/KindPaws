using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Application.DI.Injections;

public static class MediatrInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(typeof(MediatrInjection).Assembly));

        return services;
    }
}