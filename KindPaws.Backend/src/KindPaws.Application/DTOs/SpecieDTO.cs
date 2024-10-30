namespace KindPaws.Application.DTOs;

public class SpecieDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } 
    public string Description { get; init; } 
    public BreedDTO[] Breeds { get; init; }
}