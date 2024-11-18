using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Abstractions;

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