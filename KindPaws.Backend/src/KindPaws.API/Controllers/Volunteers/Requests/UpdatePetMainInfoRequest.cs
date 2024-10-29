using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record UpdatePetMainInfoRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name)
{
    public UpdatePetMainInfoCommand ToCommand(Guid id, Guid petId)
        => new(id, petId, SpecieId, BreedId, Name);
}