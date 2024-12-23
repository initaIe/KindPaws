using System.Text;
using KindPaws.Auth.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Auth.Infrastructure.Factories;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters Create(JwtBearerAuthOptions jwtBearerOptions)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtBearerOptions.Issuer,
            ValidAudience = jwtBearerOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerOptions.Key)),
            ValidateIssuer = jwtBearerOptions.ShouldValidateIssuer,
            ValidateAudience = jwtBearerOptions.ShouldValidateAudience,
            ValidateLifetime = jwtBearerOptions.ShouldValidateLifetime,
            ValidateIssuerSigningKey = jwtBearerOptions.ShouldValidateIssuerSigningKey,
            ClockSkew = TimeSpan.FromMinutes(jwtBearerOptions.ClockSkewInMinutes)
        };
    }

    public static TokenValidationParameters CreateWithoutValidationLifeTime(
        JwtBearerAuthOptions jwtBearerOptions)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtBearerOptions.Issuer,
            ValidAudience = jwtBearerOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerOptions.Key)),
            ValidateIssuer = jwtBearerOptions.ShouldValidateIssuer,
            ValidateAudience = jwtBearerOptions.ShouldValidateAudience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = jwtBearerOptions.ShouldValidateIssuerSigningKey,
            ClockSkew = TimeSpan.FromMinutes(jwtBearerOptions.ClockSkewInMinutes)
        };
    }
}