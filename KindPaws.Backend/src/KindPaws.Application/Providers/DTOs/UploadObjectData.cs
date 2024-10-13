namespace KindPaws.Application.Providers.DTOs;

public record UploadObjectData(
    string BucketName,
    string ObjectName,
    Stream ObjectStream);