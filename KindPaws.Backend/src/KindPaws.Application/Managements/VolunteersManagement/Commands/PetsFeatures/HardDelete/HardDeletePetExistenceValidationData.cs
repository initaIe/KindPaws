using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;

public record HardDeletePetExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;