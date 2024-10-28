using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Abstractions.IoC;

public interface ISpeciesRepository
{
    Task AddAsync(
        Specie specie,
        CancellationToken cancellationToken = default);

    void Delete(Specie specie);

    Task<Result<Specie, Error>> GetByIdAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default);

    Task<Result<Specie, Error>> GetByName(
        ShortName name,
        CancellationToken cancellationToken = default);
}