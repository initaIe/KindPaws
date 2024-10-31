namespace KindPaws.Application.DTOs;

public class BreedDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid SpecieId { get; init; }
}