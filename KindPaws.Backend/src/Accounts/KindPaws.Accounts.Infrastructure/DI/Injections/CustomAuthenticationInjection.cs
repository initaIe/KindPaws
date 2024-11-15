using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JwtBearerOptions = KindPaws.Accounts.Infrastructure.Options.JwtBearerOptions;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class CustomAuthenticationInjection
{
    public static IServiceCollection AddCustomAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtAccessTokenOptions = configuration.GetRequiredSection(JwtBearerOptions.SectionName)
                    .Get<JwtBearerOptions>();

                options.TokenValidationParameters = TokenValidationParametersFactory.Create(jwtAccessTokenOptions!);
            });
        
        return services;
    }
}