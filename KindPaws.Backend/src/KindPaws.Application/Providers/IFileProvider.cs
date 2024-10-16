using KindPaws.Application.Providers.DTOs;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Providers;

public interface IFileProvider
{
    Task<Result<Error>> UploadObjectsAsync(
        UploadObjectsData uploadObjectsData,
        CancellationToken cancellationToken = default);

    Task<Result<Error>> DeleteObjectAsync(
        DeleteObjectData deleteObjectData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetObjectLinkAsync(
        GetObjectData getObjectData,
        CancellationToken cancellationToken = default);
}