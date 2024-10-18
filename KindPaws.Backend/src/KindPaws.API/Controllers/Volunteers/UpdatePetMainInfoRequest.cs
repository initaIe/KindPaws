using KindPaws.Application.Volunteers.PetsHandlers.UpdateMainInfo;

namespace KindPaws.API.Controllers.Volunteers;

public record UpdatePetMainInfoRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name)
{
    public UpdatePetMainInfoCommand ToCommand(Guid id, Guid petId)
    {
        return new UpdatePetMainInfoCommand(
            id,
            petId,
            SpecieId,
            BreedId,
            Name);
    }
}