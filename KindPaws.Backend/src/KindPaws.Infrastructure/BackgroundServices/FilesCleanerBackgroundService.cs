using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs.FileProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundService> _logger;
    private readonly IMessageQueue<IEnumerable<DeleteFileData>> _messageQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FilesCleanerBackgroundService(
        ILogger<FilesCleanerBackgroundService> logger,
        IMessageQueue<IEnumerable<DeleteFileData>> messageQueue,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _messageQueue = messageQueue;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FilesCleanerBackgroundService is starting.");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        _logger.LogInformation("FilesCleanerBackgroundService created scope.");

        var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
        _logger.LogInformation("FilesCleanerBackgroundService received file provider.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("FilesCleanerBackgroundService starts cleaning files minio.");

            var deleteFilesData = await _messageQueue.ReadAsync(stoppingToken);

            foreach (var deleteFileData in deleteFilesData)
            {
                await fileProvider.DeleteObjectAsync(deleteFileData, stoppingToken);
            }

            _logger.LogInformation("FilesCleanerBackgroundService finished cleaning minio.");
            await Task.Delay(3000, stoppingToken);
        }

        await Task.CompletedTask;
    }
}