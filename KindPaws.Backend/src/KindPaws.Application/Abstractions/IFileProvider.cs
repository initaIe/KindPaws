using KindPaws.Application.DTOs.FileProvider;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.Abstractions;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, ErrorList>> UploadObjectsAsync(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default);

    Task<Result<Error>> DeleteObjectAsync(
        DeleteFileData deleteFileData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetObjectLinkAsync(
        GetFileData getFileData,
        CancellationToken cancellationToken = default);
}