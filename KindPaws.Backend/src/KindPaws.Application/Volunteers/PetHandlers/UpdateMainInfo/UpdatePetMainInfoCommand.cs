namespace KindPaws.Application.Volunteers.PetHandlers.UpdateMainInfo;

public record UpdatePetMainInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    Guid SpecieId,
    Guid BreedId,
    string Name);