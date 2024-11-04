using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Core.Dtos;

public record UploadFileData(
    string BucketName,
    FilePath FilePath,
    Stream Stream);