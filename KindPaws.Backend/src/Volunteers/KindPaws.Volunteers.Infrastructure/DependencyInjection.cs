using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.Core.Messaging;
using KindPaws.Core.Options;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Infrastructure.BackgroundServices;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using KindPaws.Volunteers.Infrastructure.MessageQueues;
using KindPaws.Volunteers.Infrastructure.Providers;
using KindPaws.Volunteers.Infrastructure.Repositories;
using KindPaws.Volunteers.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace KindPaws.Volunteers.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddUnitOfWork()
            .AddRepositories()
            .AddFileProviders()
            .AddMinio(configuration)
            .AddMessageQueues()
            .AddHostedServices()
            .AddServices();


        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<VolunteersWriteDbContext>();
        services.AddScoped<IVolunteersReadDbContext, VolunteersReadDbContext>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        return services;
    }

    private static IServiceCollection AddFileProviders(this IServiceCollection services)
    {
        services.AddScoped<IFileProvider, MinioProvider>();

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

    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<DeleteFileData>>,
            FilesCleanerMessageQueue<IEnumerable<DeleteFileData>>>();

        return services;
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroundService>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();

        return services;
    }
}