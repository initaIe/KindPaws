using KindPaws.Application.Volunteers.PetHandlers.UpdateMainInfo;

namespace KindPaws.API.Contracts.Volunteers.PetsRequests;

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