using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public record UpdatePetMainInfoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId,
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;