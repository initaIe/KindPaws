using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public record UpdatePetMainInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    Guid SpecieId,
    Guid BreedId,
    string Name)
    : ICommand
{
    public UpdatePetMainInfoExistenceCheckData ToExistenceCheckData()
        => new(VolunteerId, PetId, SpecieId, BreedId);
}