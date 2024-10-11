namespace KindPaws.Application.Providers;

public record ObjectUploadData(
    string BucketName,
    string Name,
    Stream Stream);