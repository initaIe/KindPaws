using System.Text;
using KindPaws.Accounts.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
                var jwtAccessTokenOptions = configuration.GetRequiredSection(JwtAccessTokenOptions.JwtAccessToken).Get<JwtAccessTokenOptions>()
                                 ?? throw new NullReferenceException("Missing jwt configurations.");

                options.TokenValidationParameters = TokenValidationParametersFactory.Create(jwtAccessTokenOptions);
            });
        
        return services;
    }
}