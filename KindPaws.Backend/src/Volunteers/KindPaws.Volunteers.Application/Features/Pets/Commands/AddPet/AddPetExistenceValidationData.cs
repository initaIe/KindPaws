using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

public record AddPetExistenceValidationData(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;