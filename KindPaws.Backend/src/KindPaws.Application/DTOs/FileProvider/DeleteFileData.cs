namespace KindPaws.Application.DTOs.FileProvider;

public record DeleteFileData(
    string BucketName,
    string FileName);