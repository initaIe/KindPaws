using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Abstractions.IoC;

public interface ISpeciesRepository
{
    Task AddAsync(
        Specie specie,
        CancellationToken cancellationToken = default);

    void SoftDelete(Specie specie);
    void HardDelete(Specie specie);

    Task<Result<Specie, Error>> GetByIdAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default);

    Task<Result<Specie, Error>> GetByNameAsync(
        ShortName name,
        CancellationToken cancellationToken = default);
}