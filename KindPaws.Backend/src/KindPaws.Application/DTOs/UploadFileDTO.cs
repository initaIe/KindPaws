namespace KindPaws.Application.DTOs;

public record UploadFileDTO(
    string Name,
    Stream Stream);