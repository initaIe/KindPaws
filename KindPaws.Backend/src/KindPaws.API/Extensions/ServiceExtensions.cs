using KindPaws.API.Validation;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using ILogger = Serilog.ILogger;

namespace KindPaws.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });
        
        services.AddSerilog();

        return services;
    }
}