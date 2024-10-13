using KindPaws.Application.Providers.DTOs;
using KindPaws.Application.Volunteers.Create;
using KindPaws.Domain.Shared.Others;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Providers;

public class FileService
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public FileService(
        ILogger<CreateVolunteerHandler> logger,
        IFileProvider fileProvider)
    {
        _logger = logger;
        _fileProvider = fileProvider;
    }

    public async Task<Result<Error>> UploadAsync(
        UploadObjectData data,
        CancellationToken cancellationToken = default)
    {
        var uploadResult = await _fileProvider.UploadObjectAsync(data, cancellationToken);

        if (uploadResult.IsFailure)
            return uploadResult.Error;

        return true;
    }

    public async Task<Result<string, Error>> GetLinkAsync(
        GetObjectData data,
        CancellationToken cancellationToken = default)
    {
        var getResult = await _fileProvider.GetObjectLinkAsync(data, cancellationToken);

        if (getResult.IsFailure)
            return getResult.Error;

        return getResult.Value;
    }

    public async Task<Result<Error>> DeleteAsync(
        DeleteObjectData data,
        CancellationToken cancellationToken = default)
    {
        var deleteResult = await _fileProvider.DeleteObjectAsync(data, cancellationToken);

        if (deleteResult.IsFailure)
            return deleteResult.Error;

        return true;
    }
}