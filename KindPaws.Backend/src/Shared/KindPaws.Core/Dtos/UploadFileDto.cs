namespace KindPaws.Core.Dtos;

public record UploadFileDto(
    string Name,
    Stream Stream);