using KindPaws.Pets.Application.DataModels;

namespace KindPaws.Pets.Application.Abstractions;

public interface IPetsReadDbContext
{
    IQueryable<VolunteerDataModel> Volunteers { get; }
    IQueryable<PetDataModel> Pets { get; }
    IQueryable<SpecieDataModel> Species { get; }
    IQueryable<BreedDataModel> Breeds { get; }
}