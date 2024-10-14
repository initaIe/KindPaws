namespace KindPaws.Application.Volunteers.PetHandlers.Add.DTOs;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name);