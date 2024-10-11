using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFileAsync(
        ObjectUploadData objectUploadData,
        CancellationToken cancellationToken = default);
}