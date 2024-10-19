using KindPaws.Application.Abstractions.DataBase;
using KindPaws.Application.Abstractions.Providers;
using KindPaws.Application.Species;
using KindPaws.Application.Volunteers;
using KindPaws.Infrastructure.Interceptors;
using KindPaws.Infrastructure.Options;
using KindPaws.Infrastructure.Providers;
using KindPaws.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace KindPaws.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddInterceptors()
            .AddRepositories()
            .AddMinio(configuration)
            .AddFileProviders()
            .AddApplicationDbContext();

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.Minio));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.Minio).Get<MinioOptions>()
                               ?? throw new Exception($"Missing {MinioOptions.Minio} configuration section");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    private static IServiceCollection AddInterceptors(
        this IServiceCollection services)
    {
        services.AddScoped<SoftDeleteInterceptor>();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();

        return services;
    }

    private static IServiceCollection AddFileProviders(
        this IServiceCollection services)
    {
        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, ApplicationDbContext>();

        return services;
    }
}