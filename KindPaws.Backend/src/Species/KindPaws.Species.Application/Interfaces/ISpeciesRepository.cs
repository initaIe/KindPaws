using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.AggregateRoot;

namespace KindPaws.Species.Application.Interfaces;

public interface ISpeciesRepository
{
    Task AddAsync(
        Specie specie,
        CancellationToken cancellationToken = default);

    void Delete(Specie specie);

    Task<Result<Specie, Error>> GetByIdAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default);

    Task<Result<Specie, Error>> GetByNameAsync(
        ShortName name,
        CancellationToken cancellationToken = default);
}