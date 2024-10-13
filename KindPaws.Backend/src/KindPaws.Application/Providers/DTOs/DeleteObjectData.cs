namespace KindPaws.Application.Providers.DTOs;

public record DeleteObjectData(
    string BucketName,
    string ObjectName);