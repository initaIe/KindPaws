using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPet;

public record AddPetExistenceValidationData(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;