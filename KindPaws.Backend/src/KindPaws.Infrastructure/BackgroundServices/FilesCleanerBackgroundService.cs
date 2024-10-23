using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs.FileProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FilesCleanerBackgroundService(
        ILogger<FilesCleanerBackgroundService> logger,
        IMessageQueue<IEnumerable<DeleteFileData>> messageQueue,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FilesCleanerBackgroundService is starting.");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var filesCleanerService = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            await filesCleanerService.ProcessAsync(stoppingToken);
        }

        await Task.CompletedTask;
    }
}

public class FilesCleanerService : IFilesCleanerService  // TODO: to move
{
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IMessageQueue<IEnumerable<DeleteFileData>> _messageQueue;
    private readonly IFileProvider _fileProvider;
    
    public FilesCleanerService(
        IMessageQueue<IEnumerable<DeleteFileData>> messageQueue,
        ILogger<FilesCleanerService> logger,
        IFileProvider fileProvider)
    {
        _messageQueue = messageQueue;
        _logger = logger;
        _fileProvider = fileProvider;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("FilesCleanerService starts finding unnecessary files in minio.");
        var deleteFilesData = await _messageQueue.ReadAsync(cancellationToken);

        _logger.LogInformation("FilesCleanerService starts deleting unnecessary files in minio.");
        foreach (var deleteFileData in deleteFilesData)
        {
            await _fileProvider.DeleteObjectAsync(deleteFileData, cancellationToken);
        }
        _logger.LogInformation("FilesCleanerService finished deleting unnecessary files in minio.");
    }
}