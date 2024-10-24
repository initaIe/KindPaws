using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs.FileProvider;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure.BackgroundServices;

public class FilesCleanerService : IFilesCleanerService // TODO: подумать куда засунуть
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IMessageQueue<IEnumerable<DeleteFileData>> _messageQueue;

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
            await _fileProvider.DeleteObjectAsync(deleteFileData, cancellationToken);
        _logger.LogInformation("FilesCleanerService finished deleting unnecessary files in minio.");
    }
}