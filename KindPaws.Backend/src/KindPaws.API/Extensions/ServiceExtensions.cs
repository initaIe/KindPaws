using KindPaws.API.Validation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace KindPaws.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        return services;
    }
}