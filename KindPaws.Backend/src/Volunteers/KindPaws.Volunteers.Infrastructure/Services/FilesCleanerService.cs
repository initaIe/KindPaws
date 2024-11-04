using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.Core.Messaging;
using KindPaws.Volunteers.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Infrastructure.Services;

public class FilesCleanerService : IFilesCleanerService
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