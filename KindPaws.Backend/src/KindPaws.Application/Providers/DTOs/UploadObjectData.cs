namespace KindPaws.Application.Providers.DTOs;

public record UploadObjectsData(
    IEnumerable<UploadObjectContent> UploadObjectsContent,
    string BucketName);