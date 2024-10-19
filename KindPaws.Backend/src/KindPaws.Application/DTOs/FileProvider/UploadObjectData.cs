using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.DTOs.FileProvider;

public record UploadFileData(
    string BucketName,
    FilePath FilePath,
    Stream Stream);