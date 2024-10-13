namespace KindPaws.Application.Providers.DTOs;

public record GetObjectData(
    string BucketName,
    string ObjectName);