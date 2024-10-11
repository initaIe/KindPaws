using KindPaws.API.Validation;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace KindPaws.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddLoggers()
            .AddAutoValidation();

        return services;
    }
    
    private static IServiceCollection AddLoggers(this IServiceCollection services)
    {
        services.AddSerilog();

        return services;
    }
    
    private static IServiceCollection AddAutoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        return services;
    }
}