namespace KindPaws.Application.FileProvider;

public record DeleteFileData(
    string BucketName,
    string FileName);