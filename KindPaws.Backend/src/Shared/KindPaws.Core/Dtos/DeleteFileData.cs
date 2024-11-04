namespace KindPaws.Core.Dtos;

public record DeleteFileData(
    string BucketName,
    string FileName);