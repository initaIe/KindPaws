namespace KindPaws.Application.Providers.DTOs;

public record UploadObjectContent(
    string ObjectName,
    Stream ObjectStream);