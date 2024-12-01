using KindPaws.Species.Application.DataModels;

namespace KindPaws.Species.Application.Abstractions;

public interface ISpeciesReadDbContext
{
    IQueryable<SpecieDataModel> Species { get; }
    IQueryable<BreedDataModel> Breeds { get; }
}