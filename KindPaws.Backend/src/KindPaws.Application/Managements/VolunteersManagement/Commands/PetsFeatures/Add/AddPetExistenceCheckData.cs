using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public record AddPetExistenceCheckData(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId)
    : IExistenceCheckData;