namespace KindPaws.Application.Volunteers.DTOs;

public record FileDTO(
    Stream Stream,
    string ContentType,
    string Name);