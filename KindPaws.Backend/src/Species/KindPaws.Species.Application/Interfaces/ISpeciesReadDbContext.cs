using KindPaws.Species.Contracts.Dtos;

namespace KindPaws.Species.Application.Interfaces;

public interface ISpeciesReadDbContext
{
    IQueryable<SpecieDto> Species { get; }
    IQueryable<BreedDto> Breeds { get; }
}