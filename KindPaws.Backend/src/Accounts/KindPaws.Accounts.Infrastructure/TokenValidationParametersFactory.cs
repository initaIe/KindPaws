using System.Text;
using KindPaws.Accounts.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Accounts.Infrastructure;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters Create(JwtAccessTokenOptions jwtAccessTokenOptions)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtAccessTokenOptions.Issuer,
            ValidAudience = jwtAccessTokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAccessTokenOptions.Key)),
            ValidateIssuer = jwtAccessTokenOptions.ShouldValidateIssuer,
            ValidateAudience = jwtAccessTokenOptions.ShouldValidateAudience,
            ValidateLifetime = jwtAccessTokenOptions.ShouldValidateLifetime,
            ValidateIssuerSigningKey = jwtAccessTokenOptions.ShouldValidateIssuerSigningKey,
            ClockSkew = TimeSpan.FromMinutes(jwtAccessTokenOptions.ClockSkewInMinutes)
        };
    }
    
    public static TokenValidationParameters CreateWithoutValidationLifeTime(
        JwtAccessTokenOptions jwtAccessTokenOptions)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtAccessTokenOptions.Issuer,
            ValidAudience = jwtAccessTokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAccessTokenOptions.Key)),
            ValidateIssuer = jwtAccessTokenOptions.ShouldValidateIssuer,
            ValidateAudience = jwtAccessTokenOptions.ShouldValidateAudience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = jwtAccessTokenOptions.ShouldValidateIssuerSigningKey,
            ClockSkew = TimeSpan.FromMinutes(jwtAccessTokenOptions.ClockSkewInMinutes)
        };
    }
}