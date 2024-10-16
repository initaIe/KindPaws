using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.FileProvider;

public record UploadFileData(
    string BucketName,
    FilePath FilePath,
    Stream Stream);