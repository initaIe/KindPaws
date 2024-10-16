namespace KindPaws.Application.FileProvider;

public record GetFileData(
    string BucketName,
    string FileName);