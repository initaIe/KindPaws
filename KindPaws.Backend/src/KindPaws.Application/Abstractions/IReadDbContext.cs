using KindPaws.Application.DTOs;

namespace KindPaws.Application.Abstractions;

public interface IReadDbContext
{
    IQueryable<VolunteerDTO> Volunteers { get; }
    IQueryable<PetDTO> Pets { get; }
    IQueryable<SpecieDTO> Species { get; }
    IQueryable<BreedDTO> Breeds { get; }
}