namespace KindPaws.Application.Volunteers.PetHandlers.Add;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name);