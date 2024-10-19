namespace KindPaws.Application.DTOs.FileProvider;

public record GetFileData(
    string BucketName,
    string FileName);