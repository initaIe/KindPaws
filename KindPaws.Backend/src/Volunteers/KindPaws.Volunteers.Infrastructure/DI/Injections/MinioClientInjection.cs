using KindPaws.Volunteers.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class MinioClientInjection
{
    public static IServiceCollection AddMinioClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetRequiredSection(MinioOptions.SectionName).Get<MinioOptions>()!;

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });

        return services;
    }
}